using UnityEngine;
using UnityEngine.VFX;

namespace Life.Data.Effects
{
    [CreateAssetMenu(fileName = nameof(EffectsConfig), menuName = "Game/Configs/" + nameof(EffectsConfig))]
    internal class EffectsConfig : ScriptableObject
    {
        [field: SerializeField]
        public VisualEffect EffectPrefab { get; private set; }

        [field: SerializeField]
        public VisualEffectAsset FoodEatingEffect { get; private set; }
    }
}
