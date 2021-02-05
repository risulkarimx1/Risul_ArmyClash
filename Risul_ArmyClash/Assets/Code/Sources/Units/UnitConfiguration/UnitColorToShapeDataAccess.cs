namespace Assets.Code.Sources.Units.UnitConfiguration
{
    public class UnitColorToShapeDataAccess
    {
        private readonly ColorToShapeMappingData _colorToShapeMappingData;

        public UnitColorToShapeDataAccess(ColorToShapeMappingData colorToShapeMappingData)
        {
            _colorToShapeMappingData = colorToShapeMappingData;

            var dataPath = Constants.Constants.ColorMapJsonFilePath;
            // TODO: Make it async... Create Method pattern
            _ = _colorToShapeMappingData.LoadAsync(dataPath);
        }

        public ColorToShapeMapModel GetColorShapeMappedModel(ShapeModel shapeModel, ColorModel colorModel)
        {
            return _colorToShapeMappingData.GetColorShapeMappedModel(shapeModel, colorModel);
        }
    }
}