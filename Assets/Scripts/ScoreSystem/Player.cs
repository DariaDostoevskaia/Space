using System;

namespace SpaceGame.ScoreSystem
{
    public class Player
    {
        public event Action OnScoreAdded;
        private Score _score;

        public Player(Score score)
        {
            _score = score;
        }

        public void AddScore(int value)
        {
            _score.AddValue(value);
            OnScoreAdded?.Invoke();
        }

        public int GetScore()
        {
            return _score.Value;
        }
    }
}