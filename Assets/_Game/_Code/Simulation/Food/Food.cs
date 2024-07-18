using UnityEngine;

namespace Xandudex.LifeGame
{
    internal class Food
    {
        private readonly GameObject foodObject;
        private readonly Transform foodTransform;
        public Food(GameObject foodObject)
        {
            this.foodObject = foodObject;
            this.foodTransform = foodObject.transform;
        }

        public Vector3 Position => foodTransform.position;

        public void Eat()
        {
            foodObject.SetActive(false);
        }
    }
}
