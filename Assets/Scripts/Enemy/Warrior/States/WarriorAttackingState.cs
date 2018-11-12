using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackingState : EnemyState<Warrior> {


    public override void OnStateUpdate(Warrior owner) {

        //Attack

        //Check if its on radius yet. if not return to chase
        if (!owner.statsHandler.isPlayerOnAttackRadius) {
            owner.stateMachine.SetState(new WarriorChasingState());
        }

    }

    public override void OnStateEnter(Warrior owner) {
        owner.statsHandler.isAttacking = true;
    }

    public override void OnStateExit(Warrior owner) {
        owner.statsHandler.isAttacking = false;
    }
  
}
