using SpaceGame.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Button _menuExitButton;

        [SerializeField] private TextMeshProUGUI _firstPlayerHealthText;
        [SerializeField] private TextMeshProUGUI _secondPlayerHealthText;

        [SerializeField] private TextMeshProUGUI _player1ScoreText;
        [SerializeField] private TextMeshProUGUI _player2ScoreText;

        [SerializeField] private TextMeshProUGUI _enemyShipCountText;

        public void Start()
        {
            _menuExitButton.onClick.AddListener(LoadMainMenu);
        }

        public void LoadMainMenu()
        {
            var saveService = new SaveService();
            saveService.SaveGame(GameContext.CurrentGameData);
            SceneManager.LoadScene(0);
        }

        public void SetFirstPlayerHealthText(float health)
        {
            _firstPlayerHealthText.text = $"Player 1 Health: {health}";
        }

        public void SetSecondPlayerHealthText(float health)
        {
            _secondPlayerHealthText.text = $"Player 2 Health: {health}";
        }

        public void SetFirstPlayerScoreText(int score)
        {
            _player1ScoreText.text = $"Player 1 Score: {score}";
        }

        public void SetSecondPlayerScoreText(int score)
        {
            _player2ScoreText.text = $"Player 2 Score: {score}";
        }

        public void SetEnemyCount(int count)
        {
            _enemyShipCountText.text = $"Enemy ship count: {count}";
        }

        public void OnDestroy()
        {
            _menuExitButton.onClick.RemoveAllListeners();
        }
    }
}