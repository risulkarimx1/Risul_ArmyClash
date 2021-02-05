using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units.UnitConfiguration;
using Zenject;

namespace Assets.Code.Sources.Units.Factory
{
    public class RandomUnitGenerationFactory : IFactory<UnitSide, IUnitController>
    {
        private readonly DiContainer _container;
        private readonly IUnitConfigGenerator _unitConfigGenerator;
        private readonly UnitColorToShapeDataAccess _colorToShapeDataAccess;
        private readonly GameSettings _gameSetting;

        public RandomUnitGenerationFactory(DiContainer container, IUnitConfigGenerator unitConfigGenerator,
            UnitColorToShapeDataAccess colorToShapeDataAccess, GameSettings gameSetting)
        {
            _container = container;
            _unitConfigGenerator = unitConfigGenerator;
            _colorToShapeDataAccess = colorToShapeDataAccess;
            _gameSetting = gameSetting;
        }

        public IUnitController Create(UnitSide unitSide)
        {
            var randomConfig = _unitConfigGenerator.GetConfig();

            var unitObject = _container.InstantiatePrefab(randomConfig.Item2.ShapeObject);

            var unitModel = new UnitModel(randomConfig.Item1,
                randomConfig.Item2,
                randomConfig.Item3,
                _colorToShapeDataAccess,
                _gameSetting.InitHp,
                _gameSetting.InitAtk);
            var unitView = _container.InstantiateComponent<UnitView>(unitObject);
            unitView.gameObject.name = $"{unitSide} - {unitModel}";
            return _container.Instantiate<UnitController>(new object[] {unitModel, unitView, unitSide});
        }
    }
}