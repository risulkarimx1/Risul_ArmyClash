using Sources.Units.UnitConfiguration;
using UniRx;

namespace Code.Sources.Units
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
        private readonly ShapeModel _shapeModel;
        private readonly SizeModel _sizeModel;
        private readonly ColorToShapeMappingData _colorToShapeMap;

        public ColorModel ColorModel { get; }

        public UnitModel(ColorModel colorModel, ShapeModel shapeModel, SizeModel sizeModel,
            ColorToShapeMappingData colorToShapeMap)
        {
            ColorModel = colorModel;
            _shapeModel = shapeModel;
            _sizeModel = sizeModel;
            _colorToShapeMap = colorToShapeMap;

            CalculateProperties();
        }

        private void CalculateProperties()
        {
            var colorToShapeMap = _colorToShapeMap.GetColorShapeMappedModel(_shapeModel, ColorModel);
            Hp.Value = Hp.Value + _shapeModel.Hp + _sizeModel.Hp + colorToShapeMap.Hp;
            Atk.Value = Atk.Value + _shapeModel.Atk + _sizeModel.Atk + colorToShapeMap.Atk;
        }
    }
}