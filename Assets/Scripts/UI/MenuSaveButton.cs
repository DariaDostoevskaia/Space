using SpaceGame.SaveSystem;
using SpaceGame.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveButton : MonoBehaviour
{
    [SerializeField] private Button _saveButton;

    private void Start()
    {
        _saveButton.onClick.AddListener(StartLoadedGame);
    }

    private void StartLoadedGame()
    {
        var saveService = new SaveService();
        saveService.SaveGame(GameContext.CurrentGameData);
    }

    private void OnDestroy()
    {
        _saveButton.onClick.RemoveAllListeners();
    }
}