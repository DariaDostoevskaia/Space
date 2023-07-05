﻿using UnityEngine;

namespace SpaceGame.SaveSystem.Dto
{
    [System.Serializable]
    public class EnemyData
    {
        public float Health;
        public float Count;
        public float[] Positions;
    }
}