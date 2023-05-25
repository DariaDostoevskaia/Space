namespace SpaceGame.ScoreSystem
{
    public class EnemyShipCount
    {
        public int Count { get; private set; }

        public void AddCount(int count)
        {
            Count += count;
        }
    }
}