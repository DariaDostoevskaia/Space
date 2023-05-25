using SpaceGame.ScoreSystem;
using SpaceGame.Ship;
using System.Collections;
using System.Linq;
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
        private WaitForSeconds _wait;
        private PlayerShip[] players;

        private EnemyShipCount _enemyShipCount;
        private int _count = 0;

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

        public void GetEnemyShipCount()
        {
            _count = 0;
            //_enemyShipCount.text = $"Score: {_count.AddEnemyShipCount()}";
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
                var enemyShip = CreateEnemyShip();
                _count += 1;
                enemyShip.SetTargets(players);
            }
        }

        private EnemyShip CreateEnemyShip()
        {
            var positionX = Random.Range(_minPositionX, _maxPositionX);
            var position = new Vector3(positionX, _positionY, 0);
            var enemyShip = Instantiate(_enemyShipPrefab, position, Quaternion.identity);
            return enemyShip;
        }
    }
}