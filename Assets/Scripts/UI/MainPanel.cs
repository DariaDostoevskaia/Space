using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        [SerializeField] private PlayerData _playerData;

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
            //methods;
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

    //public class SaveLoad
    //{
    //    public static List<Game> savedGames = new List<Game>();

    //    public static void Save()
    //    {
    //        SaveLoad.savedGames.Add(Game.current);
    //        BinaryFormatter bf = new BinaryFormatter();
    //        //Application.persistentDataPath это строка; выведите ее в логах и вы увидите расположение файла сохранений
    //        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
    //        bf.Serialize(file, SaveLoad.savedGames);
    //        file.Close();
    //    }

    //    public static void Load()
    //    {
    //        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
    //        {
    //            BinaryFormatter bf = new BinaryFormatter();
    //            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
    //            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
    //            file.Close();
    //        }
    //    }
    //}
}