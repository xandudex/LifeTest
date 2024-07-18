using UnityEngine;

namespace Xandudex.LifeGame
{
    internal class GameResources : IGameResources
    {
        GameResourcesConfig config;
        public GameResources()
        {
            config = Resources.LoadAll<GameResourcesConfig>("")[0];
        }
        public GameObject FoodPrefab => config.FoodPrefab;

        public GameObject AnimalPrefab => config.AnimalPrefab;
    }
}
