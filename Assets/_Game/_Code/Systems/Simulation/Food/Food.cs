using Life.Data.Effects;
using Life.Services.Effects;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

namespace Life.Systems.Simulation
{
    internal class Food
    {
        private readonly IEffectsService effectsService;
        private readonly EffectsConfig effectsConfig;
        private readonly GameObject foodObject;
        private readonly ObjectPool<Food> pool;
        private readonly CancellationToken token;
        private readonly Transform foodTransform;
        public Food(IEffectsService effectsService, EffectsConfig effectsConfig, GameObject foodObject, ObjectPool<Food> pool, CancellationToken token)
        {
            this.effectsService = effectsService;
            this.effectsConfig = effectsConfig;
            this.foodObject = foodObject;
            this.pool = pool;
            this.token = token;
            this.foodTransform = foodObject.transform;
        }

        public Vector3 Position
        {
            get => foodTransform.position;
            set => foodTransform.position = value;
        }

        public async Awaitable Eat()
        {
            await effectsService.PlayAt(effectsConfig.FoodEatingEffect, foodObject.transform.position);

            if (foodObject)
                foodObject.SetActive(false);
            pool.Release(this);
        }

        public void Activate()
        {
            foodObject.SetActive(true);
        }
    }
}
