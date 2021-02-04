using Assets.Code.Sources.GameSceneUi;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Signals;
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
        [SerializeField] private GameSceneUiView _gameSceneUiView;
        
        public override void InstallBindings()
        {
            // Disposible
            
            // Install Signal
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangeSignal>();
            Container.DeclareSignal<UnitShuffleSignal>();
            
            // Settings from scriptable objects
            Container.Bind<ColorToShapeMappingData>().FromScriptableObjectResource(Constants.Constants.ColorToShapeMapPath).AsSingle();
            Container.Bind<UnitConfigurationsData>().FromScriptableObjectResource(Constants.Constants.UnitConfigurationDataPath).AsSingle();
            Container.Bind<GameSettings>().FromScriptableObjectResource(Constants.Constants.GameSettingsPath).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitColorToShapeDataAccess>().AsSingle().NonLazy();
            
            // computed configs objects
            Container.Bind<IUnitConfigGenerator>().To<RandomUnitConfigGenerator>().AsSingle();
            // Game State Machine
            Container.Bind<GameStateController>().AsSingle();
            
            // Unit Factory
            Container.BindFactory<UnitSide, IUnitController, UnitFactory>().FromFactory<RandomUnitGenerationFactory>();
            
            // Guild Systems
            Container.Bind<GuildPositionController>().AsSingle();
            Container.Bind<GuildManager>().AsSingle();

            // Ui Bindings
            Container.Bind<GameSceneUiView>().FromComponentInNewPrefab(_gameSceneUiView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameSceneUiController>().AsSingle().NonLazy();
            
            // Game Scene Manager
            Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();
        }
    }
}