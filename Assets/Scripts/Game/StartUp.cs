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
        [SerializeField] private MousePlayerShip _player1ShipPrefab;
        [SerializeField] private KeyBoardPlayerShip _player2ShipPrefab;

        [SerializeField] private EnemyFactory _enemyFactory;

        [SerializeField] private Transform _startedPosition;

        [SerializeField] private int _scorePerEnemy = 1;

        [SerializeField] private EnemyRepository _enemyRepository;

        [SerializeField] private HUD _hud;
        [SerializeField] private float _maxHealth = 10;

        [SerializeField] private int _playersNumber = 2;
        private Player _player;

        private void Start()
        {
            _enemyRepository.OnEnemyAdded += OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved += OnEnemyCountChanged;

            var playerFactory = new PlayerFactory();

            List<Player> listOfGameObjects = new List<Player>();
            Player[] playersArray = listOfGameObjects.ToArray();

            for (int i = 0; i < _playersNumber; i++)
            {
                _player = playersArray[i];
                listOfGameObjects.Add(playersArray[i]);
            }

            var playersData = GameContext.CurrentGameData.PlayersData;

            if (playersData.Count == 2)
            {
                var playerData = playersData[0];

                _player = playerFactory.CreatePlayer(playerData);
            }
            else
            {
                _player = playerFactory.CreatePlayer();

                var playerData = playerFactory.CreatePlayerData(_player);

                playerData.Health = _maxHealth;

                playerData.Positions = new float[] { _startedPosition.position.x, _startedPosition.position.y };

                GameContext.CurrentGameData.PlayersData.Add(playerData);
            }

            _player.OnScoreAdded += OnPlayerScoreAdded;

            var playerShip = CreatePlayerShip(RandomPlayerPrefab(), _player, GameContext.CurrentGameData.PlayersData[0]);

            _player.SetShipId(playerShip.Guid);

            _enemyRepository.OnEnemyAdded += TryKillPlayers;

            playerShip.OnHealthChanged += UpdatePlayerHealth;

            UpdatePlayerHealth(playerShip.CurrentHealth);

            _enemyFactory.SetUp(new PlayerShip[] { playerShip }, _maxHealth);

            foreach (var enemyData in GameContext.CurrentGameData.EnemiesData)
            {
                _enemyFactory.SpawnEnemy(enemyData);
            }
            void TryKillPlayers(int count)
            {
                if (count == 10)
                {
                    playerShip.Damage(playerShip.CurrentHealth);
                }
            }
        }

        private PlayerShip RandomPlayerPrefab()
        {
            PlayerShip[] playersPrefab = { _player1ShipPrefab, _player2ShipPrefab };
            var playerIndex = Random.Range(0, playersPrefab.Length);
            return playersPrefab[playerIndex];
        }

        private void OnEnemyCountChanged(int count)
        {
            _hud.SetEnemyCount(count);
        }

        private void UpdatePlayerHealth(float health)
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
            return playerShip;
            void OnEnemyDestroyed()
            {
                player.AddScore(_scorePerEnemy);
            }
            void OnDestroyed()
            {
                playerShip.OnHealthChanged -= UpdatePlayerHealth;

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