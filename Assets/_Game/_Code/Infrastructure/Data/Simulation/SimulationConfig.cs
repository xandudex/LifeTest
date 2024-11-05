using System;
using UnityEngine;

namespace Life.Data.Simulation
{
    [CreateAssetMenu(fileName = "Simulation Config", menuName = "Game/Simulation Config")]
    internal class SimulationConfig : ScriptableObject
    {
        [field: SerializeField]
        public InitialValue<int> TerrainSize { get; private set; }

        [field: SerializeField]
        public InitialValue<int> AnimalsCount { get; private set; }

        [field: SerializeField]
        public InitialValue<float> AnimalsSpeed { get; private set; }

        [Serializable]
        internal class InitialValue<T>
        {
            [field: SerializeField]
            public T Value { get; private set; }

            [field: SerializeField]
            public T Min { get; private set; }

            [field: SerializeField]
            public T Max { get; private set; }
        }
    }
}
