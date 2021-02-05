using Assets.Code.Sources.Units.UnitConfiguration;
using UniRx;

namespace Assets.Code.Sources.Units
{
    public class UnitModel
    {
        private ReactiveProperty<float> _hp;
        private ReactiveProperty<float> _atk;

        private float _movementSpeed;
        private float _attackSpeed;


        private readonly ColorModel _colorModel;
        private readonly ShapeModel _shapeModel;
        private readonly SizeModel _sizeModel;
        private readonly UnitColorToShapeDataAccess _colorToShapeMapAccess;

        public ReactiveProperty<float> Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public ReactiveProperty<float> Atk
        {
            get => _atk;
            set => _atk = value;
        }

        public float MovementSpeed => _movementSpeed;
        public float AttackSpeed => _attackSpeed;

        public ColorModel ColorModel => _colorModel;
        public ShapeModel ShapeModel => _shapeModel;
        public SizeModel SizeModel => _sizeModel;

        public UnitModel(ColorModel colorModel, ShapeModel shapeModel, SizeModel sizeModel,
            UnitColorToShapeDataAccess colorToShapeMapAccess, float initHp, float initAtk)
        {
            _colorModel = colorModel;
            _shapeModel = shapeModel;
            _sizeModel = sizeModel;
            _colorToShapeMapAccess = colorToShapeMapAccess;

            _hp = new ReactiveProperty<float>(initHp);
            _atk = new ReactiveProperty<float>(initAtk);
        }

        public void Configure()
        {
            var colorToShapeMap = _colorToShapeMapAccess.GetColorShapeMappedModel(ShapeModel, ColorModel);
            _hp.Value = Hp.Value + ShapeModel.Hp + SizeModel.Hp + colorToShapeMap.Hp;
            _atk.Value = Atk.Value + ShapeModel.Atk + colorToShapeMap.Atk;
        }

        public override string ToString()
        {
            return $"{_colorModel}:{_shapeModel}:{_sizeModel}";
        }
    }
}