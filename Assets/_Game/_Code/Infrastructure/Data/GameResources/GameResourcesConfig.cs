using UnityEngine;

namespace Life.Data.GameResources
{
    [CreateAssetMenu(fileName = nameof(GameResourcesConfig), menuName = "Game/Configs/" + nameof(GameResourcesConfig))]
    internal class GameResourcesConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameObject FoodPrefab { get; private set; }

        [field: SerializeField]
        public GameObject AnimalPrefab { get; private set; }
    }
}
