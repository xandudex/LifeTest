using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace Xandudex.LifeGame
{
    internal class Food
    {
        private readonly GameObject foodObject;
        private readonly ObjectPool<Food> pool;
        private readonly CancellationToken token;
        private readonly Transform foodTransform;
        private readonly VisualEffect vfx;
        public Food(GameObject foodObject, ObjectPool<Food> pool, CancellationToken token)
        {
            this.foodObject = foodObject;
            this.pool = pool;
            this.token = token;
            this.foodTransform = foodObject.transform;
            this.vfx = foodObject.GetComponent<VisualEffect>();
        }

        public Vector3 Position
        {
            get => foodTransform.position;
            set => foodTransform.position = value;
        }

        public async Awaitable Eat()
        {
            vfx.Play();

            await Awaitable.WaitForSecondsAsync(1, token);

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
