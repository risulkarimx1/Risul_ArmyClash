using Code.Sources.Constants;
using Code.Sources.Units;
using Sources.Managers;
using Sources.Units.UnitConfiguration;
using UniRx.Async;
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
            Container.Bind<IUnitConfigGenerator>().To<RandomUnitConfigGenerator>().AsSingle();
            Container.BindFactory<UniTask<IUnitView>, UnitFactory>().FromFactory<RandomUnitGenerationFactory>();
            Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();
        }
    }
}