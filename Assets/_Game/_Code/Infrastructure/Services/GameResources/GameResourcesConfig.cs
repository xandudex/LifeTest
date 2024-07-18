using UnityEngine;

namespace Xandudex.LifeGame
{
    [CreateAssetMenu(fileName = "Resources Config", menuName = "Game/Resources Config")]
    internal class GameResourcesConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameObject FoodPrefab { get; private set; }

        [field: SerializeField]
        public GameObject AnimalPrefab { get; private set; }
    }
}
