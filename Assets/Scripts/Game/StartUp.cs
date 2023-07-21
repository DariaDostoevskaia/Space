using SpaceGame.Ship;
using SpaceGame.ScoreSystem;
using UnityEngine;
using SpaceGame.SaveSystem;
using SpaceGame.UI;
using SpaceGame.SaveSystem.Dto;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;
using System.Collections.Generic;

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
        private List<PlayerData> _playersData;

        private void Start()
        {
            _enemyRepository.OnEnemyAdded += OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved += OnEnemyCountChanged;

            var playerFactory = new PlayerFactory();

            _playersData = GameContext.CurrentGameData.PlayersData;
            while (_playersData.Count < _playersNumber)
            {
                var index = _playersNumber - _playersData.Count - 1;
                var player = playerFactory.CreatePlayer((PlayerIndex)index);

                var playerData = playerFactory.CreatePlayerData(player);

                playerData.Health = _maxHealth;
                playerData.Positions = new float[] { _startedPosition.position.x, _startedPosition.position.y };

                _playersData.Add(playerData);
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
                var playerData = _playersData[index];
                var player = playerFactory.CreatePlayer((PlayerIndex)index, playerData);

                var playerShip = CreatePlayerShip(_playerShipPrefabs[index], player, playerData);

                player.SetShipId(playerShip.Guid);

                playerShips[index] = playerShip;

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

        private PlayerShip CreatePlayerShip(PlayerShip playerShipPrefab, Player player, PlayerData playerData)
        {
            var startedPosition = new Vector3(playerData.Positions[0], playerData.Positions[1], _startedPosition.position.z);
            var playerShip = Instantiate(playerShipPrefab, startedPosition, Quaternion.identity);

            playerShip.SetHealth(playerData.Health);

            playerShip.OnHealthChanged += OnPlayerHealthChanged;
            OnPlayerHealthChanged(playerShip.CurrentHealth);

            player.OnScoreAdded += OnPlayerScoreAdded;
            OnPlayerScoreAdded(player.GetScore());

            playerShip.SetId(playerData.Id);

            playerShip.OnEnemyDestroyed += OnEnemyDestroyed;
            playerShip.OnDestroyed += OnDestroyed;

            return playerShip;
            void OnPlayerHealthChanged(float health)
            {
                if (player.Id == PlayerIndex.Second)
                    UpdateSecondPlayerHealth(health);
                if (player.Id == PlayerIndex.First)
                    UpdateFirstPlayerHealth(health);
            }
            void OnPlayerScoreAdded(int score)
            {
                if (player.Id == PlayerIndex.Second)
                    OnSecondPlayerScoreAdded(score);
                if (player.Id == PlayerIndex.First)
                    OnFirstPlayerScoreAdded(score);
            }
            void OnEnemyDestroyed()
            {
                player.AddScore(_scorePerEnemy);
            }
            void OnDestroyed()
            {
                player.OnScoreAdded -= OnPlayerScoreAdded;
                playerShip.OnHealthChanged -= OnPlayerHealthChanged;

                _playersData.Remove(playerData);

                playerShip.OnEnemyDestroyed -= OnEnemyDestroyed;
                playerShip.OnDestroyed -= OnDestroyed;
            }
        }

        private void OnEnemyCountChanged(int count)
        {
            _hud.SetEnemyCount(count);
        }

        private void UpdateFirstPlayerHealth(float health)
        {
            _hud.Set1PlayerHealthText(health);
        }

        private void UpdateSecondPlayerHealth(float health)
        {
            _hud.Set2PlayerHealthText(health);
        }

        private void OnFirstPlayerScoreAdded(int score)
        {
            _hud.Set1PlayerScoreText(score);
        }

        private void OnSecondPlayerScoreAdded(int score)
        {
            _hud.Set2PlayerScoreText(score);
        }

        private void OnDestroy()
        {
            _enemyRepository.OnEnemyAdded -= OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved -= OnEnemyCountChanged;
        }
    }
}