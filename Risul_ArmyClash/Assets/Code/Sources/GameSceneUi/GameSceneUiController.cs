using System;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Signals;
using UniRx;
using Zenject;
using static Assets.Code.Sources.Constants.Constants;

namespace Assets.Code.Sources.GameSceneUi
{
    public class GameSceneUiController : IInitializable, IDisposable
    {
        private readonly GameSceneUiView _gameSceneUiView;
        private readonly SignalBus _signalBus;

        public GameSceneUiController(GameSceneUiView gameSceneUiView, ZenjectSceneLoader sceneLoader,
            SignalBus signalBus, GameStateController gameStateController)
        {
            _gameSceneUiView = gameSceneUiView;
            _signalBus = signalBus;
            gameSceneUiView.CloseButton.OnClickAsObservable().Subscribe(_ => { sceneLoader.LoadScene(MenuSceneIndex); })
                .AddTo(gameSceneUiView);

            #region InitializeButton Events

            gameSceneUiView.PlayButton.OnClickAsObservable().Subscribe(_ =>
            {
                gameStateController.CurrentState = GameState.Battle;
            }).AddTo(gameSceneUiView);
            
            gameSceneUiView.GuildARandomPositionButton.OnClickAsObservable().Subscribe(_ =>
            {

            }).AddTo(gameSceneUiView);

            gameSceneUiView.GuildBRandomPositionButton.OnClickAsObservable().Subscribe(_ =>
            {

            }).AddTo(gameSceneUiView);

            gameSceneUiView.GuildAShuffleButton.OnClickAsObservable().Subscribe(_ =>
            {

            }).AddTo(gameSceneUiView);

            gameSceneUiView.GuildBRandomPositionButton.OnClickAsObservable().Subscribe(_ =>
            {

            }).AddTo(gameSceneUiView);

            #endregion

            #region MatchEnd Button Events


            #endregion

        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStateChangeSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangeSignal gameStateChangeSignal)
        {
            if (gameStateChangeSignal.GameState == GameState.Initialize)
            {
                _gameSceneUiView.SetInitializeUi();
            }else if (gameStateChangeSignal.GameState == GameState.Battle)
            {
                _gameSceneUiView.SetBattleModeUi();
            }else if (gameStateChangeSignal.GameState == GameState.EndBattle)
            {
                _gameSceneUiView.SetEndModeUi(gameStateChangeSignal.Message);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStateChangeSignal>(OnGameStateChanged);
        }
    }
}