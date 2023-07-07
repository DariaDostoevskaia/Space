using SpaceGame.SaveSystem.Dto;
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
                //spawn emeny public

                //тип энеми шип возврат
                //вызвать этот метод из стартапа дл€ создани€ шаблона врагов
                //у экземпл€ра возвращающий spawn enemy вызвать set Health и set Position дл€ загрузки

                //доп
                //сохр. врага когда он по€вилс€
                //сохр хп когда оно мен€етс€
                //сохр позицию когда мен€етс€
                //удалить при уничтожении
            }
        }

        public EnemyShip SpawnEnemy()
        {
            var enemyShip = CreateEnemyShip();

            enemyShip.SetHealth(_enemyData.Health);

            enemyShip.SetTargets(players);

            return enemyShip;
        }

        private EnemyShip CreateEnemyShip()
        {
            var positionX = Random.Range(_minPositionX, _maxPositionX);
            var position = new Vector3(positionX, _positionY, 0);
            var enemyShip = Instantiate(_enemyShipPrefab, position, Quaternion.identity);

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