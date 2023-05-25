using SpaceGame.Ship;
using System;

namespace SpaceGame.ScoreSystem
{
    public class Player
    {
        public event Action OnScoreAdded;

        public event Action OnHealthAdded;

        private Score _score;

        private Health _health;

        public Player(Score score)
        {
            _score = score;
        }

        public Player(Health health)
        {
            _health = health;
        }

        public void AddScore(int value)
        {
            _score.AddValue(value);
            OnScoreAdded?.Invoke();
        }

        public void AddHealth(float value)
        {
            _health.AddValueH(value);
            OnHealthAdded?.Invoke();
        }

        public int GetScore()
        {
            return _score.Value;
        }

        public float GetHealth()
        {
            return _health.Value;
        }
    }
}