using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Units;
using Zenject;

namespace Assets.Code.Sources.BattleSimulation
{
    public class BattleSimulation : ITickable
    {
        private readonly GuildManager _guildManager;
        private readonly SignalBus _sinSignalBus;
        private readonly GameState _gameState;
        private readonly TargetAssignment _targetAssignment;
        private readonly ProximalMovement _proximalMovement;

        public BattleSimulation(GuildManager guildManager,
            SignalBus sinSignalBus,
            GameState gameState,
            TargetAssignment targetAssignment,
            ProximalMovement proximalMovement)
        {
            _guildManager = guildManager;
            _sinSignalBus = sinSignalBus;
            _gameState = gameState;
            _targetAssignment = targetAssignment;
            _proximalMovement = proximalMovement;
        }

        public void Tick()
        {
            if (_gameState.CurrentState != State.Battle) return;

            // Target Job
            var nearestPos = _targetAssignment.LocateNearest(
                _guildManager.GetGuildPositions(UnitSide.SideA),
                _guildManager.GetGuildPositions(UnitSide.SideB));

            // Reposition Job
            _proximalMovement.MovementToNearest(
                _guildManager.GetUnitTransforms(UnitSide.SideA),
                nearestPos.closeToA,
                _guildManager.GetUnitSize(UnitSide.SideA),
                _guildManager.GetUnitMovementSpeed(UnitSide.SideA));
            
            _proximalMovement.MovementToNearest(
                _guildManager.GetUnitTransforms(UnitSide.SideB),
                nearestPos.closeToB,
                _guildManager.GetUnitSize(UnitSide.SideB),
                _guildManager.GetUnitMovementSpeed(UnitSide.SideB));
        }
    }
}