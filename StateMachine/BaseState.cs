namespace Library.StateMachine
{
    public abstract class BaseState<T>
    {
        public T owner;
        
        public BaseState(T o)
        {
            owner = o;
        }

        public abstract void OnEnter();

        public abstract void Execute();
        public abstract void FixedExecute();

        public abstract void OnExit();
    }
}
