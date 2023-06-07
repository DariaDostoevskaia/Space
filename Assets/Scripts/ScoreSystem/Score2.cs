namespace SpaceGame.ScoreSystem
{
    public class Score2
    {
        public int ValuePlayer2 { get; private set; }

        public void AddValuePlayer2(int value)
        {
            ValuePlayer2 += value;
        }
    }
}