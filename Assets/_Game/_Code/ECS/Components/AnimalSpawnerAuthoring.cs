using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Xandudex.LifeGame.Ecs
{
    public struct AnimalSpawner : IComponentData
    {
        public int Count;
        public Entity Prefab;
        public float3 Offset;
    }

    public class AnimalSpawnerAuthoring : MonoBehaviour
    {
        public int MaxCount;
        public GameObject Prefab;
        public Vector3 Offset;

        class Baker : Baker<AnimalSpawnerAuthoring>
        {
            public override void Bake(AnimalSpawnerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new AnimalSpawner
                {
                    Count = authoring.MaxCount,
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    Offset = authoring.Offset
                });
            }
        }
    }
}
