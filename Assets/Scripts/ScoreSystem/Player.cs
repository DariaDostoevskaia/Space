using System;

namespace SpaceGame.ScoreSystem
{
    public class Player
    {
        public event Action OnScoreAdded;

        private Score _score;
        private EnemyShipCount _enemyShipCount;

        public Player(Score score)
        {
            _score = score;
        }

        public void AddScore(int value)
        {
            _score.AddValue(value);
            OnScoreAdded?.Invoke();
        }

        public void AddEnemyShipCount(int count)
        {
            _enemyShipCount.AddCount(count);
        }

        public int GetScore()
        {
            return _score.Value;
        }
    }
}