using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorWanderState : EnemyState<Warrior> {

    public override void OnStateUpdate(Warrior owner) {

        // walk to location
        if(owner.movementHandler.currentTarget != null) {
            owner.movementHandler.ProcessMovement(false);
        }
        
        //If location is reached, go to checking state to check for chests
        if (owner.movementHandler.targetReached) {
            owner.stateMachine.SetState(new WarriorCheckingChestState());
        }

        //If player sensed go to chase
        if (owner.statsHandler.isPlayerSensed) {

        }

        //if hurt go to hurt
        if (owner.statsHandler.isHurt) {

        }
    }

    public override void OnStateEnter(Warrior owner) {
        owner.movementHandler.StartWandering();
        owner.movementHandler.DefineTarget(owner.movementHandler.currentTarget);
        
    }

    public override void OnStateExit(Warrior owner) {

    }


}
