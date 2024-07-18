using System;
using UnityEngine;

namespace Xandudex.LifeGame
{
    [Serializable]
    class AnimalSpawnerConfig
    {
        [field: SerializeField]
        public Transform Parent { get; private set; }
    }
}
