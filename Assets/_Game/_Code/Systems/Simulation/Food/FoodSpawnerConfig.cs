using System;
using UnityEngine;

namespace Life.Systems.Simulation
{
    [Serializable]
    internal class FoodSpawnerConfig
    {
        [field: SerializeField]
        public Transform Parent { get; private set; }

        [field: SerializeField]
        public int MaxSecondsToTraverse { get; private set; }
    }
}