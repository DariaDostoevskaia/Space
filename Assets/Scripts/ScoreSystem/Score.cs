namespace SpaceGame.ScoreSystem
{
    public class Score
    {
        public int Value { get; private set; }

        public Score(int value)
        {
            Value = value;
        }

        public void AddValue(int value)
        {
            Value += value;
        }
    }
}