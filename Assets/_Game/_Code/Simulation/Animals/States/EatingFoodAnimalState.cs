using UnityEngine;
using Xandudex.Utility.StateMachine;

namespace Xandudex.LifeGame
{
    internal partial class Animal
    {
        internal class EatingFoodAnimalState : BaseAnimalState, IPayloadedState<Food>

        {
            public EatingFoodAnimalState(Animal animal) : base(animal) { }

            public async void Enter(Food food)
            {
                await Awaitable.WaitForSecondsAsync(1);
                food.Eat();
                Animal.ChangeState<SearchFoodAnimalState>();
            }
        }
    }
}
