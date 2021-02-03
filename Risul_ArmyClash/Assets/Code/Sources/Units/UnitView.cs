using Sources.Units.UnitConfiguration;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Code.Sources.Units
{
    public class UnitFactory : PlaceholderFactory<UniTask<IUnitView>> { }
    
    public class RandomUnitGenerationFactory : IFactory<UniTask<IUnitView>>
    {
        private readonly DiContainer _container;
        private readonly IUnitConfigGenerator _unitConfigGenerator;
        private readonly ColorToShapeMappingData _colorToShapeMap;

        public RandomUnitGenerationFactory(DiContainer container, IUnitConfigGenerator unitConfigGenerator, ColorToShapeMappingData colorToShapeMap)
        {
            _container = container;
            _unitConfigGenerator = unitConfigGenerator;
            _colorToShapeMap = colorToShapeMap;
        }

        

        public async UniTask<IUnitView> Create()
        {
            var randomConfig = _unitConfigGenerator.GetConfig();
            var unitModel = new UnitModel(randomConfig.Item1, randomConfig.Item2, randomConfig.Item3, _colorToShapeMap);
            await unitModel.Configure();

            var unitObject = _container.InstantiatePrefab(randomConfig.Item2.ShapeObject);
            var unitView = _container.InstantiateComponent<UnitView>(unitObject);
            unitView.Configure(unitModel);
            return unitView;
        }
    }

    public interface IUnitView
    {
    }
    public class UnitView : MonoBehaviour, IUnitView
    {
        private readonly UnitModel _unitModel;
        private Renderer _renderer;
        private static readonly int Color = Shader.PropertyToID("_Color");


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }
        
        public void Configure(UnitModel unitModel)
        {
            _renderer.material.SetColor(Color, unitModel.ColorModel.Color);
        }
    }
}
