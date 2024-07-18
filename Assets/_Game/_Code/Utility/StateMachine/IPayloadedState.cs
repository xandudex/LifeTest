namespace Xandudex.Utility.StateMachine
{
    internal interface IPayloadedState<T> : IExitableState
    {
        void Enter(T payload);
    }
}
