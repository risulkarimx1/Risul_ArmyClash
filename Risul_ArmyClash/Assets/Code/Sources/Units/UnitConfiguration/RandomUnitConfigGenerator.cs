using UnityEngine;

namespace Assets.Code.Sources.Units.UnitConfiguration
{
    public class RandomUnitConfigGenerator: IUnitConfigGenerator
    {
        private readonly UnitConfigurationsData _configData;

        public RandomUnitConfigGenerator(UnitConfigurationsData configData)
        {
            _configData = configData;
        }

        private ColorModel GetRandomColor(UnitConfigurationsData configData) =>
            configData.ColorModels[Random.Range(0, configData.ColorModels.Length)];

        private SizeModel GetRandomSize(UnitConfigurationsData configData) =>
            configData.SizeModels[Random.Range(0, configData.SizeModels.Length)];

        private ShapeModel GetRandomShape(UnitConfigurationsData configData) =>
            configData.ShapeModels[Random.Range(0, configData.ShapeModels.Length)];

        public (ColorModel, ShapeModel, SizeModel) GetConfig()
        {
            return (GetRandomColor(_configData), GetRandomShape(_configData), GetRandomSize(_configData));
        }
    }
}