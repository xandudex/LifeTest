using Xandudex.Utility.StateMachine;

namespace Life.Systems.Simulation
{
    internal partial class Animal
    {
        internal class MovingToFoodAnimalState : BaseAnimalState, IPayloadedState<Food>
        {
            public MovingToFoodAnimalState(Animal animal) : base(animal) { }

            public async void Enter(Food food)
            {
                await Animal.mover.Move(food.Position);
                Animal.ChangeState<EatingFoodAnimalState, Food>(food);
            }
        }
    }
}
