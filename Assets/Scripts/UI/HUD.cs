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

        [SerializeField] private TextMeshProUGUI _playersHealthText;

        [SerializeField] private TextMeshProUGUI _playersScoreText;

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

        public void SetPlayerHealthText(float health)
        {
            _playersHealthText.text = $"Player Health: {health}";
        }

        public void SetPlayerScoreText(int score)
        {
            _playersScoreText.text = $"Player Score: {score}";
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