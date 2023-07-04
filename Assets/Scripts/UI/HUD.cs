using SpaceGame.SaveSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Button _menuExitButton;
        [SerializeField] private TextMeshPro _firstPlayerHealthText;

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

        public void OnDestroy()
        {
            _menuExitButton.onClick.RemoveAllListeners();
        }
    }
}