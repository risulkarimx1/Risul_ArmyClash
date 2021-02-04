using System;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Signals;
using Assets.Code.Sources.Units;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Code.Sources.BattleSimulation
{
    public class BattleSimulation: IDisposable,ITickable
    {
        private readonly GuildManager _guildManager;
        private readonly SignalBus _sinSignalBus;
        private readonly GameState _gameState;
        private readonly TargetAssignment _targetAssignment;

        public BattleSimulation(GuildManager guildManager, SignalBus sinSignalBus, GameState gameState, TargetAssignment targetAssignment)
        {
            _guildManager = guildManager;
            _sinSignalBus = sinSignalBus;
            _gameState = gameState;
            _targetAssignment = targetAssignment;
            _sinSignalBus.Subscribe<GameStateChangeSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangeSignal gameStateChangeSignal)
        {
            if (gameStateChangeSignal.State == State.Battle)
            {
                StartBattle();
            }else if (gameStateChangeSignal.State == State.EndBattle)
            {
                EndBattle();
            }
        }

        private void StartBattle()
        {
            Debug.Log($"start battle");
        }

        private void EndBattle()
        {
            Debug.Log($"End battle");
        }


        public void Tick()
        {
            if (_gameState.CurrentState != State.Battle) return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                var bunit = _guildManager.GuildBList[Random.Range(0, _guildManager.GuildBList.Count)];
                _guildManager.RemoveUnit(bunit);
            }

            var nearestPos = _targetAssignment.LocateNearest(_guildManager.GetGuildPositions(UnitSide.SideA),
                _guildManager.GetGuildPositions(UnitSide.SideB));
            var guildATarget = nearestPos.closeToA;
            for (int i = 0; i < _guildManager.GuildAList.Count; i++)
            {
                _guildManager.GuildAList[i].Position = Vector3.Lerp(_guildManager.GuildAList[i].Position,
                    guildATarget[i], Time.deltaTime / 2);
            }
        }

        public void Dispose()
        {
            _sinSignalBus.Unsubscribe<GameStateChangeSignal>(OnGameStateChanged);
        }
    }
}
