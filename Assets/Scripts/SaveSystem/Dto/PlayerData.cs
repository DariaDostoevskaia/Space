namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class PlayerData : SpaceShipData
    {
        public int Score;

        public override string ToString()
        {
            return $"{base.ToString()},{nameof(Score)}: {Score};\n";
        }
    }
}