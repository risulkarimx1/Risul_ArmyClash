using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units.UnitConfiguration;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Installers
{
    [CreateAssetMenu(fileName = "Project Context Installer", menuName = "Installers/Project Context Installer")]
    public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            // Settings from ScriptableObjects
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(Constants.Constants.ColorToShapeMapPath).AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(Constants.Constants.UnitConfigurationDataPath).AsSingle();
            Container.Bind<GameSettings>().FromScriptableObjectResource(Constants.Constants.GameSettingsPath).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitColorToShapeDataAccess>().AsSingle().NonLazy();
        }
    }
}
