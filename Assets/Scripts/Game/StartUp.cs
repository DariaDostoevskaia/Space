using SpaceGame.Ship;
using SpaceGame.ScoreSystem;
using TMPro;
using UnityEngine;
using SpaceGame.SaveSystem;
using System.Linq;
using SpaceGame.SaveSystem.Dto;

namespace SpaceGame.Game
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private MousePlayerShip _player1ShipPrefab;
        [SerializeField] private KeyBoardPlayerShip _player2ShipPrefab;

        [SerializeField] private EnemyFactory _enemyFactory;

        [SerializeField] private Transform _startedPosition;

        [SerializeField] private TextMeshProUGUI _player1ScoreText;
        [SerializeField] private TextMeshProUGUI _player2ScoreText;

        [SerializeField] private int _scorePerEnemy = 1;

        [SerializeField] private TextMeshProUGUI _firstPlayerHealthText;
        [SerializeField] private TextMeshProUGUI _secondPlayerHealthText;

        [SerializeField] private TextMeshProUGUI _enemyShipCountText;
        [SerializeField] private EnemyRepository _enemyRepository;

        [SerializeField] private float _maxHealth = 10;

        private void Start()
        {
            _enemyRepository.OnEnemyAdded += OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved += OnEnemyCountChanged;

            var playerFactory = new PlayerFactory();

            Player player1 = null;
            Player player2 = null;

            if (GameContext.CurrentGameData.PlayersData.Count == 2)
            {
                var playerData1 = GameContext.CurrentGameData.PlayersData[0];
                var playerData2 = GameContext.CurrentGameData.PlayersData[1];

                player1 = playerFactory.CreatePlayer(playerData1);
                player2 = playerFactory.CreatePlayer(playerData2);
            }
            else
            {
                player1 = playerFactory.CreatePlayer();
                player2 = playerFactory.CreatePlayer();

                var playerData1 = playerFactory.CreatePlayerData(player1);
                var playerData2 = playerFactory.CreatePlayerData(player2);

                playerData1.Health = playerData2.Health = _maxHealth;

                playerData1.Positions = new float[] { _startedPosition.position.x, _startedPosition.position.y };
                playerData2.Positions = new float[] { _startedPosition.position.x, _startedPosition.position.y };

                GameContext.CurrentGameData.PlayersData.Add(playerData1);
                GameContext.CurrentGameData.PlayersData.Add(playerData2);
            }

            player1.OnScoreAdded += OnPlayer1ScoreAdded;
            player2.OnScoreAdded += OnPlayer2ScoreAdded;

            var firstPlayer = CreatePlayerShip(_player1ShipPrefab, player1, GameContext.CurrentGameData.PlayersData[0]);
            var secondPlayer = CreatePlayerShip(_player2ShipPrefab, player2, GameContext.CurrentGameData.PlayersData[1]);

            _enemyRepository.OnEnemyAdded += TryKillPlayers;

            firstPlayer.OnHealthChanged += UpdateFirstPlayerHealth;
            secondPlayer.OnHealthChanged += UpdateSecondPlayerHealth;

            UpdateSecondPlayerHealth(secondPlayer.CurrentHealth);
            UpdateFirstPlayerHealth(firstPlayer.CurrentHealth);

            _enemyFactory.SetUp(new PlayerShip[] { firstPlayer, secondPlayer });

            void TryKillPlayers(int count)
            {
                if (count == 10)
                {
                    firstPlayer.Damage(firstPlayer.CurrentHealth);
                    secondPlayer.Damage(secondPlayer.CurrentHealth);
                }
            }
        }

        private void UpdateFirstPlayerHealth(float health)
        {
            _firstPlayerHealthText.text = $"Player 1 Health: {health}";
            GameContext.PlayerData1.Health = health;
        }

        private void UpdateSecondPlayerHealth(float health)
        {
            _secondPlayerHealthText.text = $"Player 2 Health: {health}";
            GameContext.PlayerData2.Health = health;
        }

        private void OnEnemyCountChanged(int count)
        {
            GameContext.EnemysData.Count = count;
            _enemyShipCountText.text = $"Enemy ship count: {count}";
        }

        private void OnPlayer1ScoreAdded(int score)
        {
            _player1ScoreText.text = $"Player 1 Score: {score}";
            GameContext.PlayerData1.Score = score;
        }

        private void OnPlayer2ScoreAdded(int score)
        {
            _player2ScoreText.text = $"Player 2 Score: {score}";
            GameContext.PlayerData2.Score = score;
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
                playerShip.OnHealthChanged -= UpdateFirstPlayerHealth;
                playerShip.OnHealthChanged -= UpdateSecondPlayerHealth;
                playerShip.OnEnemyDestroyed -= OnEnemyDestroyed;
                playerShip.OnDestroyed -= OnDestroyed;
                player.OnScoreAdded -= OnPlayer1ScoreAdded;
                player.OnScoreAdded -= OnPlayer2ScoreAdded;
            }
        }

        private void OnDestroy()
        {
            _enemyRepository.OnEnemyAdded -= OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved -= OnEnemyCountChanged;
        }
    }
}