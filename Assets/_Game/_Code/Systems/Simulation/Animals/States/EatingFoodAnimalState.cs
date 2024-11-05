using MessagePipe;
using VContainer;
using Xandudex.Utility.StateMachine;

namespace Life.Systems.Simulation
{
    internal partial class Animal
    {
        internal class EatingFoodAnimalState : BaseAnimalState, IPayloadedState<Food>

        {
            [Inject] readonly IPublisher<SimulationSystemMessages.AnimalEating> animalEatingMessage;
            public EatingFoodAnimalState(Animal animal) : base(animal) { }

            public async void Enter(Food food)
            {
                animalEatingMessage.Publish(new());
                await food.Eat();
                Animal.food = null;
                Animal.ChangeState<SearchFoodAnimalState>();
            }
        }
    }
}
