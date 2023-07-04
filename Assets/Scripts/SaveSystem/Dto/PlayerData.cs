using UnityEngine;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class PlayerData
    {
        public int Score;
        public float Health;
        public Vector3 Position;
    }
}