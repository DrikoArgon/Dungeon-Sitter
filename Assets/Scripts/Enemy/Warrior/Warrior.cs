using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy {

    public EnemyStateMachine<Warrior> stateMachine { get; set; }

    void Start() {

        stateMachine = new EnemyStateMachine<Warrior>(this);
        stateMachine.SetState(new WarriorCheckingChestState());
    }

    void Update() {

        stateMachine.Update();

    }
}
