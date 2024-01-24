namespace Library.StateMachine
{
    public abstract class BaseSubState<T, TS> where TS : BaseState<T>
    {
        public TS state;
        public T owner;
        
        public BaseSubState(TS s)
        {
            state = s;
            owner = s.owner;
        }

        public abstract void OnEnter();

        public abstract void Execute();
        public abstract void FixedExecute();

        public abstract void OnExit();
    }
}
