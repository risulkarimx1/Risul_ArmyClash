using UniRx;
using Zenject;
using static Assets.Code.Sources.Constants.Constants;

namespace Assets.Code.Sources.MainMenu
{
    public class MainMenuController
    {
        private readonly MainMenuView _mainMenuView;

        public MainMenuController(MainMenuView mainMenuView, ZenjectSceneLoader sceneLoader)
        {
            _mainMenuView = mainMenuView;
            _mainMenuView.PlayButton.OnClickAsObservable().Subscribe(_ => { sceneLoader.LoadScene(GameSceneIndex); })
                .AddTo(_mainMenuView);
        }
    }
}