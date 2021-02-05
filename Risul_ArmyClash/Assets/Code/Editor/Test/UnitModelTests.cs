using System.Linq;
using Assets.Code.Sources.Constants;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.UnitConfiguration;
using NUnit.Framework;
using UniRx;
using Zenject;

namespace Assets.Code.Editor.Test
{
    [TestFixture]
    public class UnitModelTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Initialize()
        {
            Container.Bind<CompositeDisposable>().AsSingle();
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(Constants.ColorToShapeMapPath)
                .AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(Constants.UnitConfigurationDataPath)
                .AsSingle();
            Container.Bind<GameSettings>().FromScriptableObjectResource(Constants.GameSettingsPath)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UnitColorToShapeDataAccess>().AsSingle();
        }

        [Test]
        public void FoundColorToShapeMappingDataInResourcesFolder()
        {
            var colorToShapeMapping = Container.Resolve<ColorToShapeMappingData>();
            Assert.NotNull(colorToShapeMapping);
        }

        [Test]
        public void FoundUnitConfigurationsDataInResourcesFolder()
        {
            var unitConfigurationsData = Container.Resolve<UnitConfigurationsData>();
            Assert.NotNull(unitConfigurationsData);
        }

        [Test]
        public void Blue_Cube_Small_Has_Hp_150_Attack_30()
        {
            var disposable = CreateSmallBlueCube(out var unitModel);
            
            Assert.AreEqual(150, unitModel.Hp.Value);
            Assert.AreEqual(30, unitModel.Atk.Value);
            disposable.Dispose();
        }

        [Test]
        public void MovementSpeedInterpolatesFrom_10_to_5_ForHpValue_150_to_450()
        {
            var disposable = CreateSmallBlueCube(out var unitModel);
        
            unitModel.Hp.Value = 450;
            var movementSpeed = unitModel.MovementSpeed;
            Assert.AreEqual(5, movementSpeed);

            unitModel.Hp.Value = 150;
            movementSpeed = unitModel.MovementSpeed;
            Assert.AreEqual(10, movementSpeed);
            
            disposable.Dispose();
        }

        [Test]
        public void AttackSpeedInterpolates_From_1_to_2_For_AtkValue_from_30_70()
        {
            var disposable = CreateSmallBlueCube(out var unitModel);
            
            unitModel.Atk.Value = 30;
            var attackSpeed = unitModel.AttackSpeed;
            Assert.AreEqual(1, attackSpeed);

            unitModel.Atk.Value = 70;
            attackSpeed = unitModel.AttackSpeed;
            Assert.AreEqual(2, attackSpeed);
            
            disposable.Dispose();
        }
        
        private CompositeDisposable CreateSmallBlueCube(out UnitModel unitModel)
        {
            var unitConfigurationsData = Container.Resolve<UnitConfigurationsData>();
            var colorMapDataAccess = Container.Resolve<UnitColorToShapeDataAccess>();
            var disposable = Container.Resolve<CompositeDisposable>();

            var gameSettings = Container.Resolve<GameSettings>();

            var blueColorModel = unitConfigurationsData.ColorModels.FirstOrDefault(c => c.ColorType == ColorType.Blue);
            var smallSizeModel = unitConfigurationsData.SizeModels.FirstOrDefault(s => s.SizeType == SizeType.Small);
            var cubeShapeModel = unitConfigurationsData.ShapeModels.FirstOrDefault(s => s.ShapeType == ShapeType.Cube);
            unitModel = new UnitModel(blueColorModel, cubeShapeModel, smallSizeModel, colorMapDataAccess, gameSettings, disposable);
            unitModel.Configure();
            return disposable;
        }

        [TearDown]
        public void TearDown()
        {
            Container.Unbind<ColorToShapeMappingData>();
            Container.Unbind<UnitConfigurationsData>();
        }
    }
}