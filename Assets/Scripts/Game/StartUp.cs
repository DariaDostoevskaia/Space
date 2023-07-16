using SpaceGame.Ship;
using SpaceGame.ScoreSystem;
using UnityEngine;
using SpaceGame.SaveSystem;
using SpaceGame.UI;
using SpaceGame.SaveSystem.Dto;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace SpaceGame.Game
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        [SerializeField] private Transform _startedPosition;

        [SerializeField] private int _scorePerEnemy = 1;

        [SerializeField] private EnemyRepository _enemyRepository;

        [SerializeField] private HUD _hud;
        [SerializeField] private float _maxHealth = 10;

        [SerializeField] private int _playersNumber = 2;

        [SerializeField] private PlayerShip[] _playerShipPrefabs;

        private void Start()
        {
            _enemyRepository.OnEnemyAdded += OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved += OnEnemyCountChanged;

            var playerFactory = new PlayerFactory();

            var playersData = GameContext.CurrentGameData.PlayersData;
            while (playersData.Count < _playersNumber)
            {
                var index = _playersNumber - playersData.Count - 1;
                var player = playerFactory.CreatePlayer((PlayerIndex)index);

                var playerData = playerFactory.CreatePlayerData(player);

                playerData.Health = _maxHealth;
                playerData.Positions = new float[] { _startedPosition.position.x, _startedPosition.position.y };

                playersData.Add(playerData);
            }

            var playerShips = new PlayerShip[_playersNumber];

            for (int i = 0; i < _playersNumber; i++)
            {
                CreatePlayerAndShip(i);
            }

            _enemyFactory.SetUp(playerShips, _maxHealth);
            _enemyFactory.StartSpawnEnemies();

            foreach (var enemyData in GameContext.CurrentGameData.EnemiesData)
            {
                _enemyFactory.SpawnEnemy(enemyData);
            }

            void CreatePlayerAndShip(int index)
            {
                var playerData = playersData[index];
                var player = playerFactory.CreatePlayer((PlayerIndex)index, playerData);

                player.OnScoreAdded += OnPlayerScoreAdded;

                var playerShip = CreatePlayerShip(_playerShipPrefabs[index], player, playerData);
                player.SetShipId(playerShip.Guid);

                playerShips[index] = playerShip;

                UpdateFirstPlayerHealth(playerShip.CurrentHealth);

                _enemyRepository.OnEnemyAdded += TryKillPlayers;

                void TryKillPlayers(int count)
                {
                    if (count == 10)
                    {
                        playerShip.Damage(playerShip.CurrentHealth);
                    }
                }
            }
        }

        private void OnEnemyCountChanged(int count)
        {
            _hud.SetEnemyCount(count);
        }

        private void UpdateFirstPlayerHealth(float health)
        {
            _hud.SetPlayerHealthText(health);
        }

        private void OnPlayerScoreAdded(int score)
        {
            _hud.SetPlayerScoreText(score);
        }

        private PlayerShip CreatePlayerShip(PlayerShip playerShipPrefab, Player player, PlayerData playerData)
        {
            var startedPosition = new Vector3(playerData.Positions[0], playerData.Positions[1], _startedPosition.position.z);
            var playerShip = Instantiate(playerShipPrefab, startedPosition, Quaternion.identity);

            playerShip.SetHealth(playerData.Health);

            playerShip.OnEnemyDestroyed += OnEnemyDestroyed;
            playerShip.OnDestroyed += OnDestroyed;
            playerShip.OnHealthChanged += OnPlayerHealthChanged;

            return playerShip;
            void OnEnemyDestroyed()
            {
                player.AddScore(_scorePerEnemy);
            }
            void OnPlayerHealthChanged(float health)
            {
                if (player.Id == PlayerIndex.First)
                    UpdateFirstPlayerHealth(health);
                //if (player.Id == PlayerIndex.Second)
                //    UpdateSecondPlayerHealth(health);
            }
            void OnDestroyed()
            {
                playerShip.OnHealthChanged -= OnPlayerHealthChanged;

                playerShip.OnEnemyDestroyed -= OnEnemyDestroyed;
                playerShip.OnDestroyed -= OnDestroyed;

                player.OnScoreAdded -= OnPlayerScoreAdded;
            }
        }

        private void OnDestroy()
        {
            _enemyRepository.OnEnemyAdded -= OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved -= OnEnemyCountChanged;
        }
    }
}