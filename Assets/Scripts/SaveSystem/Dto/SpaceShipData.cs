using System;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class SpaceShipData
    {
        public float Health;
        public float[] Positions;

        public Guid Id;

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id};\n" +
                $" {nameof(Health)}: {Health};\n" +
                $" {nameof(Positions)}: {string.Join(";", Positions)}\n";
        }
    }
}