using Sources.Units.UnitConfiguration;
using TMPro;
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
    }
    
    public class UnitModel: UnitBaseModel
    {
        private readonly ColorModel _colorModel;
        private readonly ShapeModel _shapeModel;
        private readonly SizeModel _sizeModel;
        private readonly ColorToShapeMappingData _colorToShapeMap;

        public UnitModel(ColorModel colorModel, ShapeModel shapeModel, SizeModel sizeModel, ColorToShapeMappingData colorToShapeMap)
        {
            _colorModel = colorModel;
            _shapeModel = shapeModel;
            _sizeModel = sizeModel;
            _colorToShapeMap = colorToShapeMap;

            CalculateProperties();
        }

        private void CalculateProperties()
        {
            var colorToShapeMap = _colorToShapeMap.GetColorShapeMappedModel(_shapeModel, _colorModel);
            _hp.Value = _hp.Value + _shapeModel.Hp + _sizeModel.Hp + colorToShapeMap.Hp;
            _atk.Value = _atk.Value + _shapeModel.Atk + _sizeModel.Atk + colorToShapeMap.Atk;
        }
    }
}
