using SpaceGame.SaveSystem;
using SpaceGame.SaveSystem.Dto;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _endGameButton;

        private void Start()
        {
            _startGameButton.onClick.AddListener(StartNewGame);
            _loadGameButton.onClick.AddListener(StartLoadedGame);
            _endGameButton.onClick.AddListener(EndGame);
        }

        private void StartNewGame()
        {
            GameContext.CurrentGameData = new GameData();
            SceneManager.LoadScene(1);
        }

        private static void StartLoadedGame()
        {
            var saveService = new SaveService();
            var gameData = saveService.LoadGame();
            GameContext.CurrentGameData = gameData;
            SceneManager.LoadScene(1);
        }

        private void EndGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _loadGameButton.onClick.RemoveAllListeners();
            _endGameButton.onClick.RemoveAllListeners();
        }
    }
}