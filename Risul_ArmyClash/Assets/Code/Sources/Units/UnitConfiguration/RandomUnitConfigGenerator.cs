using UnityEngine;

namespace Assets.Code.Sources.Units.UnitConfiguration
{
    public class RandomUnitConfigGenerator: IUnitConfigGenerator
    {
        private readonly UnitConfigurationsData _configData;
        private readonly UnitColorToShapeDataAccess _colorToShapeMapDataAccess;

        public RandomUnitConfigGenerator(UnitConfigurationsData configData, UnitColorToShapeDataAccess colorToShapeMapDataAccess)
        {
            _configData = configData;
            _colorToShapeMapDataAccess = colorToShapeMapDataAccess;
        }

        private ColorModel GetRandomColor =>
            _configData.ColorModels[Random.Range(0, _configData.ColorModels.Length)];

        private SizeModel GetRandomSize =>
            _configData.SizeModels[Random.Range(0, _configData.SizeModels.Length)];

        private ShapeModel GetRandomShape =>
            _configData.ShapeModels[Random.Range(0, _configData.ShapeModels.Length)];

        public (ColorModel, ShapeModel, SizeModel) GetConfig()
        {
            return (GetRandomColor, GetRandomShape, GetRandomSize);
        }

        public UnitModel GetRandomModel()
        {
            return new UnitModel(GetRandomColor, GetRandomShape, GetRandomSize, _colorToShapeMapDataAccess);
        }
    }
}