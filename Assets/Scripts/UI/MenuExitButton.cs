using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public class MenuExitButton : MonoBehaviour
    {
        [SerializeField] private Button _menuExitButton;

        public void Start()
        {
            _menuExitButton.onClick.AddListener(MenuExit);
        }

        public void MenuExit()
        {
            SceneManager.LoadScene(0);
        }

        public void OnDestroy()
        {
            _menuExitButton.onClick.RemoveAllListeners();
        }
    }
}