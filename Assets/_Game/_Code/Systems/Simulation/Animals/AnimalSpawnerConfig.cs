using System;
using UnityEngine;

namespace Life.Systems.Simulation
{
    [Serializable]
    class AnimalSpawnerConfig
    {
        [field: SerializeField]
        public Transform Parent { get; private set; }
    }
}
