using SpaceGame.Ship;
using SpaceGame.ScoreSystem;
using TMPro;
using UnityEngine;

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

        [SerializeField] private int _scorePlayer1PerEnemy = 1;
        [SerializeField] private int _scorePlayer2PerEnemy = 1;

        [SerializeField] private TextMeshProUGUI _firstPlayerHealthText;
        [SerializeField] private TextMeshProUGUI _secondPlayerHealthText;

        [SerializeField] private TextMeshProUGUI _enemyShipCountText;
        [SerializeField] private EnemyRepository _enemyRepository;

        private Player player1;
        private Player player2;

        private void Start()
        {
            _enemyRepository.OnEnemyAdded += OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved += OnEnemyCountChanged;

            var score = new Score();
            var score2 = new Score();

            player1 = new Player(score);
            player2 = new Player(score2);

            player1.OnScoreAdded += OnPlayer1ScoreAdded;
            player2.OnScoreAdded += OnPlayer2ScoreAdded;

            var firstPlayer = CreatePlayerShip(_player1ShipPrefab, player1);
            var secondPlayer = CreatePlayerShip(_player2ShipPrefab, player2);
            _enemyRepository.OnEnemyAdded += TryKillPlayers;

            firstPlayer.OnHealthChanged += UpdateFirstPlayerHealth;
            secondPlayer.OnHealthChanged += UpdateSecondPlayerHealth;
            UpdateFirstPlayerHealth(firstPlayer.CurrentHealth);
            UpdateSecondPlayerHealth(secondPlayer.CurrentHealth);

            _enemyFactory.SetUp(new PlayerShip[] { firstPlayer, secondPlayer });
            _enemyFactory.StartSpawnEnemies();

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
        }

        private void UpdateSecondPlayerHealth(float health)
        {
            _secondPlayerHealthText.text = $"Player 2 Health: {health}";
        }

        private void OnEnemyCountChanged(int count)
        {
            _enemyShipCountText.text = $"Enemy ship count: {count}";
        }

        private void OnPlayer1ScoreAdded()
        {
            _player1ScoreText.text = $"Player 1 Score: {player1.GetScore()}";
        }

        private void OnPlayer2ScoreAdded()
        {
            _player2ScoreText.text = $"Player 2 Score: {player2.GetScore()}";
        }

        private PlayerShip CreatePlayerShip(PlayerShip playerShipPrefab, Player player)
        {
            var playerShip = Instantiate(playerShipPrefab, _startedPosition.position, Quaternion.identity);

            playerShip.OnEnemyDestroyed += OnEnemyDestroyed;
            playerShip.OnDestroyed += OnDestroyed;
            return playerShip;
            void OnEnemyDestroyed()
            {
                player.AddScore(_scorePlayer1PerEnemy);
                player2.AddScore(_scorePlayer2PerEnemy);
            }
            void OnDestroyed()
            {
                playerShip.OnHealthChanged -= UpdateFirstPlayerHealth;
                playerShip.OnHealthChanged -= UpdateSecondPlayerHealth;
                playerShip.OnEnemyDestroyed -= OnEnemyDestroyed;
                playerShip.OnDestroyed -= OnDestroyed;
            }
        }

        private void OnDestroy()
        {
            player1.OnScoreAdded -= OnPlayer1ScoreAdded;
            player2.OnScoreAdded -= OnPlayer2ScoreAdded;

            _enemyRepository.OnEnemyAdded -= OnEnemyCountChanged;
            _enemyRepository.OnEnemyRemoved -= OnEnemyCountChanged;
        }
    }
}