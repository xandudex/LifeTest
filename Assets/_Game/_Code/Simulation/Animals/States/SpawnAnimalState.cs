using Xandudex.Utility.StateMachine;

namespace Xandudex.LifeGame
{
    internal partial class Animal
    {
        internal class SpawnAnimalState : BaseAnimalState, IState
        {
            public SpawnAnimalState(Animal animal) : base(animal) { }

            public void Enter()
            {
                Animal.ChangeState<SearchFoodAnimalState>();
            }
        }
    }
}
