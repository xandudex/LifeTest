using Unity.Entities;
using UnityEngine;

namespace Xandudex.LifeGame.Ecs
{
    internal struct FoodConfig : IComponentData
    {
        public Entity Prefab;
    }

    internal class FoodConfigAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        class Baker : Baker<FoodConfigAuthoring>
        {
            public override void Bake(FoodConfigAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                Entity prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic);

                AddComponent(entity, new FoodConfig
                {
                    Prefab = prefab
                });
            }
        }
    }
}
