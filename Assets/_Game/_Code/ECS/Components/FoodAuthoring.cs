using Unity.Entities;
using UnityEngine;

namespace Xandudex.LifeGame.Ecs
{
    internal struct Food : IComponentData { }

    internal class FoodAuthoring : MonoBehaviour
    {
        class Baker : Baker<FoodAuthoring>
        {
            public override void Bake(FoodAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Food());
            }
        }
    }
}
