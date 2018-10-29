using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine<T> {
    public EnemyState<T> currentState;
    public T owner;

    public EnemyStateMachine(T owner) {
        this.owner = owner;
        currentState = null;
    }

    public void SetState(EnemyState<T> newstate) {
        if (currentState != null) {
            currentState.OnStateExit(owner);
        }

        currentState = newstate;

        if (currentState != null) {
            currentState.OnStateEnter(owner);
        }
    }

    public void Update() {
        if (currentState != null)
            currentState.OnStateUpdate(owner);
    }
}
