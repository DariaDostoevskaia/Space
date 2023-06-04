using SpaceGame.Ship;
using SpaceGame.ScoreSystem;
using TMPro;
using UnityEngine;
using System;

namespace SpaceGame.Game
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private MousePlayerShip _playerShipPrefab;
        [SerializeField] private KeyBoardPlayerShip _player2ShipPrefab;

        [SerializeField] private EnemyFactory _enemyFactory;

        [SerializeField] private Transform _startedPosition;

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private int _scorePerEnemy = 1;

        [SerializeField] private TextMeshProUGUI _enemyShipCountText;
        [SerializeField] private EnemyRepository _enemyRepository;

        private Player player;

        private void Start()
        {
            _enemyRepository.OnEnemyAdded += OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved += OnEnemyCountChanged;
            var score = new Score();
            player = new Player(score);
            player.OnScoreAdded += OnScoreAdded;
            var firstPlayer = CreatePlayerShip(_playerShipPrefab, player);
            var secondPlayer = CreatePlayerShip(_player2ShipPrefab, player);
            _enemyFactory.SetUp(new PlayerShip[] { firstPlayer, secondPlayer });
            _enemyFactory.StartSpawnEnemies();
        }

        private void OnEnemyCountChanged(int count)
        {
            _enemyShipCountText.text = $"Enemy ship count: {count}";
        }

        private void OnScoreAdded()
        {
            _scoreText.text = $"Score: {player.GetScore()}";
        }

        private PlayerShip CreatePlayerShip(PlayerShip playerShipPrefab, Player player)
        {
            var playerShip = Instantiate(playerShipPrefab, _startedPosition.position, Quaternion.identity);
            playerShip.OnEnemyDestroyed += OnEnemyDestroyed;
            return playerShip;
            void OnEnemyDestroyed()
            {
                playerShip.OnEnemyDestroyed -= OnEnemyDestroyed;
                player.AddScore(_scorePerEnemy);
            }
        }

        private void OnDestroy()
        {
            player.OnScoreAdded -= OnScoreAdded;
            _enemyRepository.OnEnemyAdded -= OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved -= OnEnemyCountChanged;
        }
    }
}