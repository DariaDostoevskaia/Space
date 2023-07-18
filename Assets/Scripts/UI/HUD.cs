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

        [SerializeField] private TextMeshProUGUI _player1HealthText;
        [SerializeField] private TextMeshProUGUI _player2HealthText;

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
            SceneManager.LoadScene((int)Scene.Menu);
        }

        public void Set1PlayerHealthText(float health)
        {
            _player1HealthText.text = $"Player 1 Health: {health}";
        }

        public void Set2PlayerHealthText(float health)
        {
            _player2HealthText.text = $"Player 2 Health: {health}";
        }

        public void Set1PlayerScoreText(int score)
        {
            _player1ScoreText.text = $"Player 1 Score: {score}";
        }

        public void Set2PlayerScoreText(int score)
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