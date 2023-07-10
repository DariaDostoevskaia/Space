using SpaceGame.SaveSystem;
using SpaceGame.SaveSystem.Dto;
using SpaceGame.Ship;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGame.Game
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyShip _enemyShipPrefab;
        [SerializeField] private float _minPositionX;
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _positionY;
        [SerializeField] private float _spawnDelay = 10f;

        [SerializeField] private EnemyRepository _enemyRepository;

        private WaitForSeconds _wait;
        private PlayerShip[] players;

        private void Start()
        {
            _wait = new WaitForSeconds(_spawnDelay);
        }

        public void SetUp(PlayerShip[] playerShips)
        {
            players = playerShips;
        }

        public void StartSpawnEnemies()
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }

        private bool HasAlivePlayer()
        {
            return players
                .Any(player => player != null
                && player.CurrentHealth > 0);
        }

        private IEnumerator SpawnEnemyCoroutine()
        {
            while (HasAlivePlayer())
            {
                yield return _wait;
                var enemyData = new SpaceShipData();

                enemyData.Positions = new[] { enemyShip.transform.position.x, enemyShip.transform.position.y };
                enemyData.Health = enemyShip.CurrentHealth;
                enemyData.Id = enemyShip.Guid;

                GameContext.CurrentGameData.EnemiesData.Add(enemyData);
                SpawnEnemy(enemyData);
            }
        }

        public EnemyShip SpawnEnemy(SpaceShipData enemyData)
        {
            var enemyShip = CreateEnemyShip();
            enemyShip.SetTargets(players);

            enemyShip.SetHealth(enemyData.Health);
            enemyShip.SetPositions(enemyData.Positions);

            return enemyShip;
        }

        private EnemyShip CreateEnemyShip()
        {
            var positionX = Random.Range(_minPositionX, _maxPositionX);
            var position = new Vector3(positionX, _positionY, 0);
            var enemyShip = Instantiate(_enemyShipPrefab, position, Quaternion.identity);

            enemyShip.SetId(Guid.NewGuid());

            _enemyRepository.Add(enemyShip);

            enemyShip.OnDestroyed += OnDestroyed;
            return enemyShip;

            void OnDestroyed()
            {
                enemyShip.OnDestroyed -= OnDestroyed;
                _enemyRepository.Remove(enemyShip);
            }
        }
    }
}