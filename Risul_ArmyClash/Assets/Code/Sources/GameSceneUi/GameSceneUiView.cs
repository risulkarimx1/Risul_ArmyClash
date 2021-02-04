using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Sources.GameSceneUi
{
    public class GameSceneUiView : MonoBehaviour
    {
        [Header("Initialize Ui")]
        [SerializeField] private GameObject _initializePanel;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _guildAShuffleButton;
        [SerializeField] private Button _guildBShuffleButton;
        [SerializeField] private Button _guildARandomPositionButton;
        [SerializeField] private Button guildBRandomPositionButton;

        [Space(10)]
        [Header("Battle Ui")]
        [SerializeField] private GameObject _battleUiPanel;
        [SerializeField] private TextMeshProUGUI _teamAScoreText;
        [SerializeField] private TextMeshProUGUI _teamBScroreText;
        
        [Space(10)]
        [Header("Game Over Ui")]
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _matchResultText;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _homeButton;
        
        public Button CloseButton => _closeButton;
        public Button PlayButton => _playButton;
        public Button GuildAShuffleButton => _guildAShuffleButton;
        public Button GuildBShuffleButton => _guildBShuffleButton;
        public Button GuildARandomPositionButton => _guildARandomPositionButton;
        public Button GuildBRandomPositionButton => guildBRandomPositionButton;
        
        public Button ResetButton => _resetButton;

        public Button HomeButton => _homeButton;

        public void SetInitializeUi()
        {
            _gameOverPanel.SetActive(false);
            _battleUiPanel.SetActive(false);
            _initializePanel.SetActive(true);
        }

        public void SetBattleModeUi()
        {
            _gameOverPanel.SetActive(false);
            _initializePanel.SetActive(false);
            _battleUiPanel.SetActive(true);
        }

        private void UpdateBattleModeTexts(string teamAScore, string teamBScore)
        {
            _teamAScoreText.text = teamAScore;
            _teamBScroreText.text = teamBScore;
        }
        
        public void SetEndModeUi(string matchResultText)
        {
            _initializePanel.SetActive(false);
            _battleUiPanel.SetActive(false);
            _gameOverPanel.SetActive(true);
            _matchResultText.text = matchResultText;
        }
    }
}
