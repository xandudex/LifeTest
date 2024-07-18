using Xandudex.Utility.StateMachine;

namespace Xandudex.LifeGame
{
    internal abstract class BaseAnimalState : IExitableState
    {
        public readonly Animal Animal;
        protected BaseAnimalState(Animal animal)
        {
            this.Animal = animal;
        }

        public virtual void Exit() { }
    }
}
