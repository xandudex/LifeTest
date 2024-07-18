using System;
using UnityEngine;

namespace Xandudex.LifeGame
{
    [Serializable]
    internal class GameBoardSettings
    {
        [field: SerializeField]
        public uint Size { get; set; }

        [field: SerializeField]
        public uint AnimalsCount { get; set; }

        [field: SerializeField]
        public uint AnimalsSpeed { get; set; }
    }
}
