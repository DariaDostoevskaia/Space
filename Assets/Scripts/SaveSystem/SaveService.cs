using Newtonsoft.Json;
using SpaceGame.SaveSystem.Dto;
using UnityEngine;

namespace SpaceGame.SaveSystem
{
    public class SaveService
    {
        public void SaveGame(GameData gameData)
        {
            var json = JsonConvert.SerializeObject(gameData);

            PlayerPrefs.SetString(nameof(SpaceGame), json);
            PlayerPrefs.Save();
        }

        public GameData LoadGame()
        {
            var json = PlayerPrefs.GetString(nameof(SpaceGame));
            var gameData = JsonConvert.DeserializeObject<GameData>(json);
            return gameData;
        }
    }
}