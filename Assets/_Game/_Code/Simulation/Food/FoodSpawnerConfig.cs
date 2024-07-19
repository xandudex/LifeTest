﻿using System;
using UnityEngine;

namespace Xandudex.LifeGame
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