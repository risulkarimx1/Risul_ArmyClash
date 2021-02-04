using Assets.Code.Sources.GameStateMachine;

namespace Assets.Code.Sources.Signals
{
    public class GameStateChangeSignal
    {
        public GameState GameState { get; set; }
        public string Message { get; set; }
    }
}
