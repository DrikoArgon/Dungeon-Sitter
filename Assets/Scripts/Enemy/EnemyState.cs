
public abstract class EnemyState<T> {
    public virtual void OnStateEnter(T _owner) { }
    public virtual void OnStateExit(T _owner) { }
    public abstract void OnStateUpdate(T _owner);
}
