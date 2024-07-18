namespace Xandudex.Utility.StateMachine
{

    internal interface IState : IExitableState
    {
        void Enter();
    }
}
