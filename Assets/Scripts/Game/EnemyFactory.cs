using SpaceGame.SaveSystem;
using SpaceGame.SaveSystem.Dto;
using SpaceGame.Ship;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

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

        private EnemyData _enemyData;
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

                SpawnEnemy();
            }
        }

        public EnemyShip SpawnEnemy()
        {
            var enemyShip = CreateEnemyShip();
            enemyShip.SetTargets(players);

            enemyShip.SetHealth(_enemyData.Health);
            enemyShip.SetPositions(_enemyData.Positions);

            return enemyShip;
        }

        private EnemyShip CreateEnemyShip()
        {
            var positionX = Random.Range(_minPositionX, _maxPositionX);
            var position = new Vector3(positionX, _positionY, 0);
            var enemyShip = Instantiate(_enemyShipPrefab, position, Quaternion.identity);

            GameContext.count += 1;

            _enemyRepository.Add(enemyShip);

            enemyShip.OnDestroyed += OnDestroyed;
            return enemyShip;

            void OnDestroyed()
            {
                enemyShip.OnDestroyed -= OnDestroyed;
                _enemyRepository.Remove(enemyShip);

                PlayerPrefs.DeleteKey(GameContext.EnemysData.ToString());
                GameContext.EnemysData.Count -= 1;
            }
        }
    }
}