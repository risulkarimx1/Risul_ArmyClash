using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public class GameSceneManager: IInitializable
    {
        private readonly GuildManager _guildManager;
        private readonly GameState _gameState;


        public GameSceneManager(GuildManager guildManager, GameState gameState)
        {
            _guildManager = guildManager;
            _gameState = gameState;
        }

        public void Initialize()
        {
            _guildManager.CreateGuilds();
            _gameState.CurrentState = State.Initialize;
        }
    }
}
