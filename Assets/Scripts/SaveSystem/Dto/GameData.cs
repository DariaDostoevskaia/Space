using System.Collections.Generic;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class GameData
    {
        public List<PlayerData> PlayersData = new List<PlayerData>();
        public List<EnemyData> EnemiesData = new List<EnemyData>();
    }
}