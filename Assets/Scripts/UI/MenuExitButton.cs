using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuExitButton : MonoBehaviour
{
    [SerializeField] private Button _menuExitButton;

    private void Start()
    {
        _menuExitButton.onClick.AddListener(MenuExit);
    }

    private void MenuExit()
    {
        SceneManager.LoadScene(5);
    }

    private void OnDestroy()
    {
        _menuExitButton.onClick.RemoveAllListeners();
    }
}