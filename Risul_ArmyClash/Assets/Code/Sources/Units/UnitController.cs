using UnityEngine;

namespace Assets.Code.Sources.Units
{
    public class UnitController: IUnitController
    {
        private UnitModel _unitModel;
        private readonly UnitView _unitView;
        private readonly UnitSide _unitSide;
        
        public UnitSide UnitSide => _unitSide;

        public UnitController(UnitModel unitModel, UnitView unitView,UnitSide unitSide)
        {
            _unitModel = unitModel;
            _unitView = unitView;
            _unitSide = unitSide;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
        }
        

        public void Configure(UnitModel unitModel)
        {
            _unitModel = unitModel;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
        }

        public void SetPosition(Vector3 position) => _unitView.SetPosition(position);
    }
}
