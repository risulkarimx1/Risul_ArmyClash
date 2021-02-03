using Assets.Code.Sources.Units.UnitConfiguration;
using UniRx;
using UniRx.Async;

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
        }

        public async UniTask Configure()
        {
            var colorToShapeMap = await _colorToShapeMap.GetColorShapeMappedModelAsync(_shapeModel, ColorModel);
            _hp.Value = Hp.Value + _shapeModel.Hp + _sizeModel.Hp + colorToShapeMap.Hp;
            _atk.Value = Atk.Value + _shapeModel.Atk + colorToShapeMap.Atk;
        }
    }
}