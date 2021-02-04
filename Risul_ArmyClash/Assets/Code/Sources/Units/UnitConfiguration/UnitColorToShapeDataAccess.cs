namespace Assets.Code.Sources.Units.UnitConfiguration
{
    public class UnitColorToShapeDataAccess
    {
        private readonly ColorToShapeMappingData _colorToShapeMappingData;

        public UnitColorToShapeDataAccess(ColorToShapeMappingData colorToShapeMappingData)
        {
            _colorToShapeMappingData = colorToShapeMappingData;
            // Make it call from other thread
            _ = _colorToShapeMappingData.LoadAsync(Constants.Constants.ColorMapJsonFilePath);
        }

        public ColorToShapeMapModel GetColorShapeMappedModel(ShapeModel shapeModel, ColorModel colorModel)
        {
            return _colorToShapeMappingData.GetColorShapeMappedModel(shapeModel, colorModel);
        }
    }
}
