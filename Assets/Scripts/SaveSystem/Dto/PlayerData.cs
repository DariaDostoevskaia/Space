namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class PlayerData
    {
        public int Score;
        public float Health;
        public float[] Positions;

        public override string ToString()
        {
            return $"{nameof(Score)}: {Score};\n" +
                $" {nameof(Health)}: {Health};\n" +
                $" {nameof(Positions)}: {string.Join(";", Positions)}\n";
        }
    }
}