namespace SpaceGame.ScoreSystem
{
    public class Health
    {
        public float Value { get; private set; }

        public void AddValueH(float value)
        {
            Value += value;
        }
    }
}