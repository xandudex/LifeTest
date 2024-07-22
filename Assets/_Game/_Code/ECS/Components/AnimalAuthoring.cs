using Unity.Entities;
using UnityEngine;

namespace Xandudex.LifeGame.Ecs
{
    public struct SearchingFood : IComponentData { }

    public struct HasFood : IComponentData
    {
        public Entity Food;
    }

    public struct Animal : IComponentData { }

    public class AnimalAuthoring : MonoBehaviour
    {
        class Baker : Baker<AnimalAuthoring>
        {
            public override void Bake(AnimalAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Animal());
            }
        }
    }
}
