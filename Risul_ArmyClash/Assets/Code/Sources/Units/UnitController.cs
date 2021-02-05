using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Code.Sources.Units
{
    public class UnitController : IUnitController
    {
        private UnitModel _unitModel;
        private readonly UnitView _unitView;
        private readonly UnitSide _unitSide;
        private readonly UnitWeapon _unitWeapon;
        private bool _isAlive;

        public UnitSide UnitSide => _unitSide;
        public bool IsAlive => _isAlive;
        public Transform Transform => _unitView.Transform;
        public float Size => _unitModel.SizeModel.SizeFactor;

        public UnitController(UnitModel unitModel, UnitView unitView, UnitSide unitSide, UnitWeapon unitWeapon)
        {
            _unitModel = unitModel;
            _unitView = unitView;
            _unitSide = unitSide;
            _unitWeapon = unitWeapon;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
            _isAlive = true;
            _unitWeapon.Configure(unitView,unitSide);
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

        public float3 Rotation
        {
            get => _unitView.Transform.eulerAngles;
            set => _unitView.Transform.eulerAngles = value;
        }

        public void Hit()
        {
            _unitView.Rigidbody.AddRelativeForce( - _unitView.Transform.forward + (Vector3.right * Random.Range(-3, 3)) * Random.Range(5,10), ForceMode.Impulse);
        }

        public int GetId()
        {
            return _unitView.GetID();
        }

        public void KillUnit()
        {
            _isAlive = false;
            _unitView.SetActive(false);
        }
    }
}