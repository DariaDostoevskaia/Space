using System;

namespace SpaceGame.ScoreSystem
{
    public class Player2
    {
        public event Action OnScoreAddedPlayer2;

        private Score2 _score2;

        public Player2(Score2 score2)
        {
            _score2 = score2;
        }

        public void AddScorePlayer2(int value)
        {
            _score2.AddValuePlayer2(value);
            OnScoreAddedPlayer2?.Invoke();
        }

        public int GetScorePlayer2()
        {
            return _score2.ValuePlayer2;
        }
    }
}