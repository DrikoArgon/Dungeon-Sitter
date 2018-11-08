
public abstract class EnemyState<T> {
    public virtual void OnStateEnter(T owner) { }
    public virtual void OnStateExit(T owner) { }
    public abstract void OnStateUpdate(T owner);
}
