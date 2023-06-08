using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }
}