using SpaceGame.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveButton : MonoBehaviour
{
    [SerializeField] private Button _saveButton;

    private void Start()
    {
        _saveButton.onClick.AddListener(Save);
    }

    private void Save()
    {
        var saveService = new SaveService();
        saveService.SaveGame(GameContext.CurrentGameData);
        Debug.Log($"Save:\n {GameContext.CurrentGameData}");
    }

    private void OnDestroy()
    {
        _saveButton.onClick.RemoveAllListeners();
    }
}