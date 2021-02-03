using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using Assets.Code.Sources.Units.UnitConfiguration;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Installers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(Constants.Constants.ColorToShapeMapPath).AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(Constants.Constants.UnitConfigurationDataPath).AsSingle();
            Container.Bind<GameSettings>().FromScriptableObjectResource(Constants.Constants.GameSettingsPath).AsSingle();
            
            Container.Bind<IUnitConfigGenerator>().To<RandomUnitConfigGenerator>().AsSingle();
            Container.Bind<GuildManager>().AsSingle();
            
            Container.BindFactory<UnitSide, IUnitController, UnitFactory>().FromFactory<RandomUnitGenerationFactory>();
            Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();
        }
    }
}