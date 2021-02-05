using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using UniRx;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public class GameSceneManager : IInitializable, ILateDisposable
    {
        private readonly GuildManager _guildManager;
        private readonly GameState _gameState;
        private readonly CompositeDisposable _disposable;


        public GameSceneManager(GuildManager guildManager, GameState gameState, CompositeDisposable disposable)
        {
            _guildManager = guildManager;
            _gameState = gameState;
            _disposable = disposable;
        }

        public void Initialize()
        {
            _guildManager.CreateGuilds();
            _gameState.CurrentState = State.Initialize;
        }

        public void LateDispose()
        {
            _disposable.Dispose();
        }
    }
}