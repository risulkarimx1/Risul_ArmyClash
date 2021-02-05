using Assets.Code.Sources.MainMenu;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Installers
{
    [CreateAssetMenu(fileName = "MenuSceneInstaller", menuName = "Installers/MenuSceneInstaller")]
    public class MenuSceneInstaller : ScriptableObjectInstaller<MenuSceneInstaller>
    {
        [SerializeField] private GameObject _mainMenuView;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(_mainMenuView).AsSingle().NonLazy();
            Container.Bind<MainMenuController>().AsSingle().NonLazy();
        }
    }
}