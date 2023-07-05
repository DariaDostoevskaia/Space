using SpaceGame.SaveSystem.Dto;

namespace SpaceGame.SaveSystem
{
    public static class GameContext
    {
        public static GameData CurrentGameData { get; set; }

        public static PlayerData PlayerData1 => CurrentGameData.PlayersData[0];

        public static PlayerData PlayerData2 => CurrentGameData.PlayersData[1];

        public static EnemyData EnemysData => CurrentGameData.EnemiesData[int.MaxValue]; //primer
    }
}