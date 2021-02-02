using Code.Sources.Constants;
using Code.Sources.Units;
using Sources.Managers;
using Sources.Units.UnitConfiguration;
using UnityEngine;
using Zenject;

namespace Sources.Installers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(Constants.ColorToShapeMapPath).AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(Constants.UnitConfigurationDataPath).AsSingle();
            Container.BindFactory<IUnitView, UnitFactory>().FromFactory<RandomUnitGenerationFactory>();
            Container.Bind<GameSceneManager>().AsSingle();

            var gc = Container.Resolve<GameSceneManager>();
            Debug.Log($"gc");
        }
    }
}