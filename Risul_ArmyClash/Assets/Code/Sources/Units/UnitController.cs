using Unity.Mathematics;
using UnityEngine;

namespace Assets.Code.Sources.Units
{
    public class UnitController: IUnitController
    {
        private UnitModel _unitModel;
        private readonly UnitView _unitView;
        private readonly UnitSide _unitSide;
        
        public UnitSide UnitSide => _unitSide;

        public bool IsAlive => _isAlive;
        public Transform Transform => _unitView.Transform;
        public float Size => _unitModel.SizeModel.SizeFactor;

        private bool _isAlive;

        public UnitController(UnitModel unitModel, UnitView unitView,UnitSide unitSide)
        {
            _unitModel = unitModel;
            _unitView = unitView;
            _unitSide = unitSide;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
            _isAlive = true;
        }


        public void Configure(UnitModel unitModel)
        {
            _unitModel = unitModel;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
        }

        public float3 Position
        {
            get => _unitView.Position;
            set => _unitView.Position = value;
        }

        public void KillUnit()
        {
            _isAlive = false;
            _unitView.SetActive(false);
        }
    }
}
