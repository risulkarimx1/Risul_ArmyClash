using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Units;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public class GameSceneManager: IInitializable, ITickable
    {
        private readonly GuildManager _guildManager;
        private readonly GameStateController _gameStateController;


        public GameSceneManager(GuildManager guildManager, GameStateController gameStateController)
        {
            _guildManager = guildManager;
            _gameStateController = gameStateController;
        }

        public void Initialize()
        {
            _guildManager.CreateGuilds();
            _gameStateController.CurrentState = GameState.Initialize;
        }


        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _guildManager.ShuffleUnits(UnitSide.SideA);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                _guildManager.ShuffleUnits(UnitSide.SideB);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _guildManager.ShufflePositions(UnitSide.SideA);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _guildManager.ShufflePositions(UnitSide.SideB);
            }
        }
    }
}
