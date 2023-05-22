using System;
using UnityEngine;

namespace SpaceGame.ScoreSystem
{
    public class Score
    {
        public int Value { get; private set; }

        public void AddValue(int value)
        {
            Value += value;
        }
    }
}