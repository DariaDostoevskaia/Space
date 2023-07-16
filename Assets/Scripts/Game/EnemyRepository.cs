using SpaceGame.SaveSystem;
using SpaceGame.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceGame.Game
{
    public class EnemyRepository : MonoBehaviour
    {
        public event Action<int> OnEnemyRemoved;

        public event Action<int> OnEnemyAdded;

        private List<EnemyShip> _enemyShips = new List<EnemyShip>();

        public void Add(EnemyShip enemyShip)
        {
            _enemyShips.Add(enemyShip);

            OnEnemyAdded?.Invoke(_enemyShips.Count);
        }

        public void Remove(EnemyShip enemyShip)
        {
            _enemyShips.Remove(enemyShip);

            var enemyData = GameContext.CurrentGameData.EnemiesData.First(enemyData => enemyData.Id == enemyShip.Guid);
            GameContext.CurrentGameData.EnemiesData.Remove(enemyData);

            OnEnemyRemoved?.Invoke(_enemyShips.Count);
        }
    }
}