using System;
using UnityEngine;

namespace Xandudex.LifeGame
{
    [Serializable]
    internal class SimulationSettings : ISaveData
    {
        public readonly uint Size;
        public readonly uint AnimalsCount;
        public readonly float AnimalsSpeed;
        public SimulationSettings(int size, int animalsCount, float animalsSpeed)
        {
            Size = (uint)size;
            AnimalsCount = (uint)animalsCount;
            AnimalsSpeed = animalsSpeed;
        }

        public Rect WorldBorders => new Rect(new Vector2(-Size / 2f, -Size / 2f), new Vector2(Size, Size));
    }
}
