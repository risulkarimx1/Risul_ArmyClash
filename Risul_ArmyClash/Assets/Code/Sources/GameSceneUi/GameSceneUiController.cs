using System;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Signals;
using Assets.Code.Sources.Units;
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
            SignalBus signalBus, GameState gameState)
        {
            _gameSceneUiView = gameSceneUiView;
            _signalBus = signalBus;
            gameSceneUiView.CloseButton.OnClickAsObservable().Subscribe(_ => { sceneLoader.LoadScene(MenuSceneIndex); })
                .AddTo(gameSceneUiView);

            #region InitializeButton Events

            gameSceneUiView.PlayButton.OnClickAsObservable().Subscribe(_ =>
            {
                gameState.CurrentState = State.Battle;
            }).AddTo(gameSceneUiView);
            
            gameSceneUiView.GuildARandomPositionButton.OnClickAsObservable().Subscribe(_ =>
            {
                _signalBus.Fire(new UnitShuffleSignal
                {
                    ShuffleType = ShuffleType.Position,
                    UnitSide = UnitSide.SideA
                });
            }).AddTo(gameSceneUiView);

            gameSceneUiView.GuildBRandomPositionButton.OnClickAsObservable().Subscribe(_ =>
            {
                _signalBus.Fire(new UnitShuffleSignal
                {
                    ShuffleType = ShuffleType.Position,
                    UnitSide = UnitSide.SideB
                });
                
            }).AddTo(gameSceneUiView);

            gameSceneUiView.GuildAShuffleButton.OnClickAsObservable().Subscribe(_ =>
            {
                _signalBus.Fire(new UnitShuffleSignal
                {
                    ShuffleType = ShuffleType.Units,
                    UnitSide = UnitSide.SideA
                });
            }).AddTo(gameSceneUiView);

            gameSceneUiView.GuildBShuffleButton.OnClickAsObservable().Subscribe(_ =>
            {
                _signalBus.Fire(new UnitShuffleSignal
                {
                    ShuffleType = ShuffleType.Units,
                    UnitSide = UnitSide.SideB
                });
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
            if (gameStateChangeSignal.State == State.Initialize)
            {
                _gameSceneUiView.SetInitializeUi();
            }else if (gameStateChangeSignal.State == State.Battle)
            {
                _gameSceneUiView.SetBattleModeUi();
            }else if (gameStateChangeSignal.State == State.EndBattle)
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