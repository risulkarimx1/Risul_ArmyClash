using System.Linq;
using Code.Sources.Constants;
using Code.Sources.Units;
using NUnit.Framework;
using Sources.Units.UnitConfiguration;
using Zenject;

namespace Code.Editor.Test
{
    [TestFixture]
    public class UnitModelTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Initialize()
        {
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(Constants.ColorToShapeMapPath)
                .AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(Constants.UnitConfigurationDataPath)
                .AsSingle();
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
        public void Blue_Cube_Small_Has_Hp_150_Attack_15()
        {
            var unitConfigurationsData = Container.Resolve<UnitConfigurationsData>();
            var colorToShapeMapping = Container.Resolve<ColorToShapeMappingData>();
            var blueColorModel = unitConfigurationsData.ColorModels.FirstOrDefault(c => c.ColorType == ColorType.Blue);
            var smallSizeModel = unitConfigurationsData.SizeModels.FirstOrDefault(s => s.SizeType == SizeType.Small);
            var cubeShapeModel = unitConfigurationsData.ShapeModels.FirstOrDefault(s => s.ShapeType == ShapeType.Cube);
            var unitModel = new UnitModel(blueColorModel, cubeShapeModel, smallSizeModel, colorToShapeMapping);
            _= unitModel.Configure();
            var attack = unitModel.Atk;
            var hp = unitModel.Hp;

            Assert.AreEqual(hp.Value, 150);
            Assert.AreEqual(attack.Value, 30);
        }

        [TearDown]
        public void TearDown()
        {
            Container.Unbind<ColorToShapeMappingData>();
            Container.Unbind<UnitConfigurationsData>();
        }
    }
}