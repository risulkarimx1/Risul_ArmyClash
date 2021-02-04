using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public class GameSceneManager: IInitializable
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
    }
}
