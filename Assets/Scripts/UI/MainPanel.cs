using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _endGameButton;

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _loadGameButton.onClick.AddListener(LoadGame);
        _endGameButton.onClick.AddListener(EndGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    private void EndGame()
    {
        SceneManager.LoadScene(3);
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _loadGameButton.onClick.RemoveAllListeners();
        _endGameButton.onClick.RemoveAllListeners();
    }
}