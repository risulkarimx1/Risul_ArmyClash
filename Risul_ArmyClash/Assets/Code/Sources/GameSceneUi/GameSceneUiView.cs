using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Sources.GameSceneUi
{
    public class GameSceneUiView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        public Button CloseButton => _closeButton;
    }
}
