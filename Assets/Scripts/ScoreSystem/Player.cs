using UnityEngine;

namespace SpaceGame.ScoreSystem
{
    public class Player
    {
        private Score _score;

        public Player(Score score)
        {
            _score = score;
        }

        public void AddScore(int value)
        {
            _score.AddValue(value);
        }
    }
}