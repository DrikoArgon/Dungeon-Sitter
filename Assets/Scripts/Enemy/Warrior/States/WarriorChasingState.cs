using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChasingState : EnemyState<Warrior> {

    public override void OnStateUpdate(Warrior owner) {

    }

    public override void OnStateEnter(Warrior owner) {
        owner.movementHandler.StartChasing();
    }

    public override void OnStateExit(Warrior owner) {

    }
}
