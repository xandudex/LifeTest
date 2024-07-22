using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Xandudex.LifeGame.Ecs
{

    partial struct AnimalSpawningSystem : ISystem
    {
        [BurstCompile]
        void ISystem.OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AnimalSpawner>();
        }

        [BurstCompile]
        void ISystem.OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            AnimalSpawner animalSpawner = SystemAPI.GetSingleton<AnimalSpawner>();

            Random random = new Random(123);
            float3 offset = animalSpawner.Offset;

            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            for (int i = 0; i < animalSpawner.Count; i++)
            {
                Entity entity = entityCommandBuffer.Instantiate(animalSpawner.Prefab);
                entityCommandBuffer.SetComponent(entity, new LocalTransform
                {
                    Position = random.NextFloat3(-offset, +offset),
                    Rotation = quaternion.identity,
                    Scale = 1
                });
            }

            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}