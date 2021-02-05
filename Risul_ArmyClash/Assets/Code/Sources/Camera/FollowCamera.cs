using System;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Camera
{
    public class FollowCamera: ITickable, IDisposable
    {
        private readonly GuildManager _guildManager;
        private readonly SignalBus _signalBus;
        private readonly GameState _gameState;
        private readonly Transform _cameraTransform;
        private Vector3 _offset;
        public FollowCamera(GuildManager guildManager, SignalBus signalBus, GameState gameState)
        {
            _guildManager = guildManager;
            _signalBus = signalBus;
            _gameState = gameState;
            if (UnityEngine.Camera.main != null) _cameraTransform = UnityEngine.Camera.main.transform;
            _signalBus.Subscribe<GameStateChangeSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangeSignal gameStateChangeSignal)
        {
            if (gameStateChangeSignal.State == State.Battle)
            {
                _offset = _cameraTransform.position - GetAveragePosition();
            }
        }

        public void Tick()
        {
            if(_gameState.CurrentState != State.Battle) return;
            
            var averagePosition = GetAveragePosition();

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _offset + averagePosition, Time.deltaTime) ;
        }

        private Vector3 GetAveragePosition()
        {
            var centerVector = Vector3.zero;
            foreach (var unitController in _guildManager.GuildAList)
            {
                centerVector += (Vector3) unitController.Position;
            }

            foreach (var unitController in _guildManager.GuildBList)
            {
                centerVector += (Vector3)unitController.Position;
            }

            var averagePosition = centerVector / (_guildManager.GuildAList.Count + _guildManager.GuildBList.Count);
            return averagePosition;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStateChangeSignal>(OnGameStateChanged);
        }
    }
}
