using System;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.BattleSimulation
{
    public class BattleSimulationManager: IDisposable
    {
        private readonly GuildManager _guildManager;
        private readonly SignalBus _sinSignalBus;

        public BattleSimulationManager(GuildManager guildManager, SignalBus sinSignalBus)
        {
            _guildManager = guildManager;
            _sinSignalBus = sinSignalBus;
            _sinSignalBus.Subscribe<GameStateChangeSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangeSignal gameStateChangeSignal)
        {
            if (gameStateChangeSignal.GameState == GameState.Battle)
            {
                StartBattle();
            }else if (gameStateChangeSignal.GameState == GameState.EndBattle)
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


        public void Dispose()
        {
            _sinSignalBus.Unsubscribe<GameStateChangeSignal>(OnGameStateChanged);
        }
    }
}
