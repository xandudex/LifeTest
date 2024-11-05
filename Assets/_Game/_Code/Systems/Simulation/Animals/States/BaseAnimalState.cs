using Xandudex.Utility.StateMachine;

namespace Life.Systems.Simulation
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
