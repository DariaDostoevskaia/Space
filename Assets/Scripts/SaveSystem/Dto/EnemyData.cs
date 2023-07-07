using UnityEngine;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class EnemyData
    {
        public float Health;
        public float Count;
        public float[] Positions;

        public override string ToString()
        {
            return $"{nameof(Count)}: {Count};\n" +
                $" {nameof(Health)}: {Health};\n" +
                $" {nameof(Positions)}: {string.Join(";", Positions)}\n";
        }
    }
}