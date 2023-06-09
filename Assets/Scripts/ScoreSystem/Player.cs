using System;

namespace SpaceGame.ScoreSystem
{
    public class Player
    {
        public event Action<int> OnScoreAdded;

        private Score _score;

        public Player(Score score)
        {
            _score = score;
        }

        public void AddScore(int value)
        {
            _score.AddValue(value);
            OnScoreAdded?.Invoke(_score.Value);
        }
    }
}