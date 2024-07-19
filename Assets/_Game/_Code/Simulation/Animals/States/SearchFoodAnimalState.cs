using MessagePipe;
using R3;
using System;
using Xandudex.Utility.StateMachine;

namespace Xandudex.LifeGame
{
    internal partial class Animal
    {
        internal class SearchFoodAnimalState : BaseAnimalState, IState
        {
            IDisposable disposable;

            private readonly ISubscriber<FoodSpawned> foodSpawnedSub;

            public SearchFoodAnimalState(Animal animal, ISubscriber<FoodSpawned> foodSpawnedSub) : base(animal)
            {
                this.foodSpawnedSub = foodSpawnedSub;
            }

            public void Enter()
            {
                disposable = foodSpawnedSub.Subscribe(x =>
                    MoveTo(x.Food), x => x.Animal == Animal);
            }

            public override void Exit()
            {
                disposable?.Dispose();
            }

            void MoveTo(Food food)
            {
                Animal.food = food;
                Animal.ChangeState<MovingToFoodAnimalState, Food>(food);
            }
        }
    }
}
