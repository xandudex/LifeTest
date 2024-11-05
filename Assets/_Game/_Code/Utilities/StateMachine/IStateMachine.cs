namespace Xandudex.Utility.StateMachine
{
    internal interface IStateMachine<T> where T : IExitableState
    {
        T CurrentState { get; }
        void ChangeState<K>() where K : T, IState;
        void ChangeState<K, P>(P Payload) where K : T, IPayloadedState<P>;
    }
}
