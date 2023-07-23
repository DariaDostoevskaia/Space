using SpaceGame.ScoreSystem;
using System.Linq;

namespace SpaceGame.SaveSystem.Dto
{
    public class ScoreRecord
    {
        public PlayerIndex playerIndex;
        public int score;

        public ScoreRecord(PlayerIndex index)
        {
            var playerData = GameContext.CurrentGameData.PlayersData;

            if (PlayerIndex.First == index)
            {
                var firstPlayerDataIndex = 0;
                playerIndex = PlayerIndex.First;
                playerData[firstPlayerDataIndex].Score = score;
            }

            if (PlayerIndex.Second == index)
            {
                var secondPlayerDataIndex = 1;
                playerIndex = PlayerIndex.Second;
                playerData[secondPlayerDataIndex].Score = score;
            }
        }
    }
}