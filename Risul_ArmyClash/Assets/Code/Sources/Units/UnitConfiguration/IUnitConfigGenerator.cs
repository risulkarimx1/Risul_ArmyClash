namespace Assets.Code.Sources.Units.UnitConfiguration
{
    public interface IUnitConfigGenerator
    {
        (ColorModel, ShapeModel, SizeModel) GetConfig();
    }
}