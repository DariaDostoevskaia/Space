using UnityEngine;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class PlayerData
    {
        public int Score;
        public float Health;
        //public Vector3 Position;

        public override string ToString()
        {
            return $"{nameof(Score)}: {Score}, {nameof(Health)}: {Health}";
        }
    }
}