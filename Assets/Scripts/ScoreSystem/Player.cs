using SpaceGame.SaveSystem;
using System;
using System.Linq;

namespace SpaceGame.ScoreSystem
{
    public class Player
    {
        public event Action<int> OnScoreAdded;

        private Score _score;
        private Guid _spaceShipId;

        public Player(Score score)
        {
            _score = score;
        }

        public void SetShipId(Guid spaceShipId)
        {
            _spaceShipId = spaceShipId;
        }

        public void AddScore(int value)
        {
            _score.AddValue(value);

            var playerData = GameContext.CurrentGameData.PlayersData.First(playerData => playerData.Id == _spaceShipId);
            playerData.Score = _score.Value;
            OnScoreAdded?.Invoke(_score.Value);
        }

        public int GetScore()
        {
            return _score.Value;
        }
    }
}