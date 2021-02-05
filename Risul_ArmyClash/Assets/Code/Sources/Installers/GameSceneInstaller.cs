using Assets.Code.Sources.BattleSimulation;
using Assets.Code.Sources.Camera;
using Assets.Code.Sources.FX;
using Assets.Code.Sources.GameSceneUi;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Signals;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using Assets.Code.Sources.Units.UnitConfiguration;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Installers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        [SerializeField] private GameSceneUiView _gameSceneUiView;
        [SerializeField] private HitEffect _hitEffectPrefab;
        
        public override void InstallBindings()
        {
            // Global Composite disposable 
            Container.Bind<CompositeDisposable>().AsSingle();
            
            // Install Signal
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangeSignal>();
            Container.DeclareSignal<UnitShuffleSignal>();
            Container.DeclareSignal<UnitHitSignal>();
            Container.DeclareSignal<UnitKilledSignal>();
            Container.DeclareSignal<GuildScoreUpdatedSignal>();
            
            // computed configs objects and Factory
            Container.Bind<HitEffect>().FromComponentInNewPrefab(_hitEffectPrefab).AsTransient();
            Container.Bind<IUnitConfigGenerator>().To<RandomUnitConfigGenerator>().AsSingle();
            
            // Game State Machine
            Container.Bind<GameState>().AsSingle();

            // Unit Weapon and Unit Factory Factory
            
            Container.Bind<UnitHitHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitWeapon>().AsTransient().WhenInjectedInto<UnitController>();
            Container.BindFactory<UnitSide, IUnitController, UnitFactory>().FromFactory<RandomUnitGenerationFactory>();
            
            // Guild Systems
            Container.Bind<GuildPositionController>().AsSingle();
            Container.Bind<GuildManager>().AsSingle();

            // Ui Bindings
            Container.Bind<GameSceneUiModel>().AsSingle().NonLazy();
            Container.Bind<GameSceneUiView>().FromComponentInNewPrefab(_gameSceneUiView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameSceneUiController>().AsSingle().NonLazy();
            
            // Battle Simulation
            Container.BindInterfacesAndSelfTo<BattleSimulation.BattleSimulation>().AsSingle();
            Container.Bind<TargetAssignment>().AsSingle();
            Container.Bind<ProximalMovement>().AsSingle();
            
            // Camera Follow
            Container.BindInterfacesAndSelfTo<FollowCamera>().AsSingle();
            
            // Game Scene Manager
            Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();
        }
    }
}