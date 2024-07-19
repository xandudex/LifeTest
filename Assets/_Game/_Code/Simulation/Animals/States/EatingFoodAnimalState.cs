﻿using Xandudex.Utility.StateMachine;

namespace Xandudex.LifeGame
{
    internal partial class Animal
    {
        internal class EatingFoodAnimalState : BaseAnimalState, IPayloadedState<Food>

        {
            public EatingFoodAnimalState(Animal animal) : base(animal) { }

            public async void Enter(Food food)
            {
                await food.Eat();
                Animal.food = null;
                Animal.ChangeState<SearchFoodAnimalState>();
            }
        }
    }
}