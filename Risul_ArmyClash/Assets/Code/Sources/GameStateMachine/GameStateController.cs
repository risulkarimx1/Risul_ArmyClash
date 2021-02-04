using Assets.Code.Sources.Signals;
using Zenject;

namespace Assets.Code.Sources.GameStateMachine
{
    public class GameStateController
    {
        private readonly SignalBus _signalBus;
        private GameState _currentState;

        public GameStateController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public GameState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                _signalBus.Fire(new GameStateChangeSignal()
                {
                    GameState = _currentState
                });
            }
        }
    }
}