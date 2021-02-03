using Assets.Code.Sources.Managers;

namespace Assets.Code.Sources.Units.UnitConfiguration
{
    public class UnitController
    {
        private readonly UnitModel _unitModel;
        private readonly UnitView _unitView;
        private readonly UnitSide _unitSide;
        public UnitSide UnitSide => _unitSide;


        public UnitController(UnitModel unitModel, UnitView unitView,UnitSide unitSide)
        {
            _unitModel = unitModel;
            _unitView = unitView;
            _unitSide = unitSide;
        }
    }
}
