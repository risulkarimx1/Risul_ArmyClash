using Assets.Code.Sources.Units.UnitConfiguration;
using UniRx;

namespace Assets.Code.Sources.Units
{
    public class UnitBaseModel
    {
        protected ReactiveProperty<float> _hp;
        protected ReactiveProperty<float> _atk;

        public UnitBaseModel()
        {
            _hp = new ReactiveProperty<float>(100);
            _atk = new ReactiveProperty<float>(10);
        }

        public ReactiveProperty<float> Hp => _hp;

        public ReactiveProperty<float> Atk => _atk;
    }

    public class UnitModel : UnitBaseModel
    {
        private readonly ColorModel _colorModel;
        private readonly ShapeModel _shapeModel;
        private readonly SizeModel _sizeModel;
        private readonly UnitColorToShapeDataAccess _colorToShapeMapAccess;

        public ColorModel ColorModel => _colorModel;
        public ShapeModel ShapeModel => _shapeModel;
        public SizeModel SizeModel => _sizeModel;

        public UnitModel(ColorModel colorModel, ShapeModel shapeModel, SizeModel sizeModel,
            UnitColorToShapeDataAccess colorToShapeMapAccess)
        {
            _colorModel = colorModel;
            _shapeModel = shapeModel;
            _sizeModel = sizeModel;
            _colorToShapeMapAccess = colorToShapeMapAccess;
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