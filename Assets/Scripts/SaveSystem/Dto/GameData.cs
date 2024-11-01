﻿using System.Collections.Generic;
using System.Linq;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class GameData
    {
        public List<PlayerData> PlayersData = new List<PlayerData>();
        public List<SpaceShipData> EnemiesData = new List<SpaceShipData>();

        public override string ToString()
        {
            var players = string.Join(", ", PlayersData
                .Select(playersData => playersData
                .ToString()));

            var enemies = string.Join(", ", EnemiesData
                .Select(enemiesData => enemiesData
                .ToString()));

            var allPlayers = players + "\n" + enemies;

            return allPlayers;
        }
    }
}