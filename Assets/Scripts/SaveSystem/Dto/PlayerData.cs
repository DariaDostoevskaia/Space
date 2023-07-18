namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class PlayerData : SpaceShipData
    {
        public int Score;

        public override string ToString()
        {
            base.ToString();
            return $"{nameof(Score)}: {Score};\n";
        }
    }
}