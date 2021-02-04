using UniRx;
using Zenject;
using static Assets.Code.Sources.Constants.Constants;

namespace Assets.Code.Sources.GameSceneUi
{
    public class GameSceneUiController
    {
        public GameSceneUiController(GameSceneUiView gameSceneUiView, ZenjectSceneLoader sceneLoader)
        {
            gameSceneUiView.CloseButton.OnClickAsObservable().Subscribe(_ =>
            {
                sceneLoader.LoadScene(MenuSceneIndex);
            }).AddTo(gameSceneUiView);
        }
    }
}