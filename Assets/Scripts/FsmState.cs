public abstract class FsmState<T>
{
    protected T owner;

    public virtual void Initialize(T owner)
    {
        this.owner = owner;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public abstract void UpdateState();
}