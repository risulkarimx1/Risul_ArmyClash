using Assets.Code.Sources.GameStateMachine;

namespace Assets.Code.Sources.Signals
{
    public class GameStateChangeSignal
    {
        public State State { get; set; }
        public string Message { get; set; }
    }
}
