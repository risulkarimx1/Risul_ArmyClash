using Assets.Code.Sources.Signals;
using Zenject;

namespace Assets.Code.Sources.GameStateMachine
{
    public class GameState
    {
        private readonly SignalBus _signalBus;
        private State _currentState;

        public GameState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                _signalBus.Fire(new GameStateChangeSignal()
                {
                    State = _currentState
                });
            }
        }
    }
}