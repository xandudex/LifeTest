using Unity.Burst;
using Unity.Entities;

namespace Xandudex.LifeGame.Ecs
{
    partial struct AnimalFoodSearchSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Animal>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
            foreach ((Animal _, Entity entity) in SystemAPI.Query<Animal>().WithNone<SearchingFood, HasFood>().WithEntityAccess())
            {
                entityCommandBuffer.AddComponent<SearchingFood>(entity);
            }

            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }

        /* [BurstCompile]
         public partial struct MovingJob : IJobEntity
         {
             public float deltaTime;
             public void Execute(ref LocalTransform transform, in Animal animal)
             {
                 //transform = transform.Translate(new float3 { z = 1 } * deltaTime).RotateY(deltaTime);
             }
         }*/
    }
}
