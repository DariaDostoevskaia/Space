using SpaceGame.SaveSystem.Dto;
using SpaceGame.ScoreSystem;

namespace SpaceGame.SaveSystem
{
    public class PlayerFactory
    {
        public Player CreatePlayer(PlayerData playerData = null)
        {
            var scoreValue = playerData?.Score ?? 0;
            var score = new Score(scoreValue);
            var player = new Player(score);
            return player;
        }

        public PlayerData CreatePlayerData(Player player)
        {
            var playerData = new PlayerData();
            playerData.Score = player.GetScore();
            return playerData;
        }
    }
}