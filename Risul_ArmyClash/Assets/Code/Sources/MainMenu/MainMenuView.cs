using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Sources.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        public Button PlayButton => _playButton;
    }
}
