using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Xandudex.LifeGame.Ecs
{
    partial struct FoodSpawningSystem : ISystem
    {
        [BurstCompile]
        void ISystem.OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SearchingFood>();
        }

        [BurstCompile]
        void ISystem.OnUpdate(ref SystemState state)
        {
            FoodConfig foodConfig = SystemAPI.GetSingleton<FoodConfig>();

            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach ((SearchingFood _, Entity animal) in SystemAPI.Query<SearchingFood>().WithEntityAccess())
            {
                Entity food = entityCommandBuffer.Instantiate(foodConfig.Prefab);
                entityCommandBuffer.RemoveComponent<SearchingFood>(animal);
                entityCommandBuffer.AddComponent(animal, new HasFood
                {
                    Food = food
                });
            }

            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}