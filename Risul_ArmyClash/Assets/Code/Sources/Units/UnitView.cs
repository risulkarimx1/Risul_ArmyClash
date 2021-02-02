using System;
using Sources.Units.UnitConfiguration;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Sources.Units
{ 
    public class UnitFactory : PlaceholderFactory<IUnitView> { }
    
    public class RandomUnitGenerationFactory : IFactory<IUnitView>
    {
        private readonly UnitConfigurationsData _configData;
        private readonly ColorToShapeMappingData _colorToShapeMap;
        
        private readonly DiContainer _container;

        public RandomUnitGenerationFactory(DiContainer container, UnitConfigurationsData configData, ColorToShapeMappingData colorToShapeMap)
        {
            _container = container;
            _configData = configData;
            _colorToShapeMap = colorToShapeMap;
        }

        private ColorModel GetRandomColor(UnitConfigurationsData configData) =>
            configData.ColorModels[Random.Range(0, configData.ColorModels.Length - 1)];

        private SizeModel GetRandomSize(UnitConfigurationsData configData) =>
            configData.SizeModels[Random.Range(0, configData.SizeModels.Length - 1)];

        private ShapeModel GetRandomShape(UnitConfigurationsData configData) =>
            configData.ShapeModels[Random.Range(0, configData.ShapeModels.Length - 1)];

        public IUnitView Create()
        {
            var colorModel = GetRandomColor(_configData);
            var sizeModel = GetRandomSize(_configData);
            var shapeModel = GetRandomShape(_configData);
            var unitModel = new UnitModel(colorModel, shapeModel, sizeModel, _colorToShapeMap);
            var unitObject = _container.InstantiatePrefab(shapeModel.ShapeObject);
            var unitView = unitObject.AddComponent<UnitViewView>();
            unitView.AssignModel(unitModel);
            return unitView;
        }
    }

    public interface IUnitView
    {
    }
    public class UnitViewView : MonoBehaviour, IUnitView
    {
        private readonly UnitModel _unitModel;
        private Renderer _renderer;
        private static readonly int Color = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }
        
        public void AssignModel(UnitModel unitModel)
        {
            Debug.Log($"Unit model {unitModel.Hp} and {unitModel.Atk}");
            _renderer.material.SetColor(Color, unitModel.ColorModel.Color);
        }
    }
}
