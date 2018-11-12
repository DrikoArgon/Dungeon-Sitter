using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChasingState : EnemyState<Warrior> {

    public override void OnStateUpdate(Warrior owner) {

        owner.movementHandler.ProcessMovement(true);
        owner.movementHandler.DefineTarget(owner.battleHandler.target.transform);

        if (owner.statsHandler.isPlayerOnAttackRadius) {
            owner.stateMachine.SetState(new WarriorAttackingState());
        }


    }

    public override void OnStateEnter(Warrior owner) {
        owner.movementHandler.StartChasing();
        owner.movementHandler.DefineTarget(owner.battleHandler.target.transform);
    }

    public override void OnStateExit(Warrior owner) {

    }
}
