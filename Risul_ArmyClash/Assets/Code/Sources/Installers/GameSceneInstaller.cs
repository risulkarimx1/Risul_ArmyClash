using Sources.Units.UnitConfiguration;
using UnityEngine;
using Zenject;

namespace Sources.Installers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        private const string _colorToShapeMapPath = "Data/UnitConfigs/ColorToShapeMap";
        private const string _unitConfigurationDataPath = "Data/UnitConfigs/UnitConfigurationData";


        public override void InstallBindings()
        {
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(_colorToShapeMapPath).AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(_unitConfigurationDataPath).AsSingle();
        }
    }
}