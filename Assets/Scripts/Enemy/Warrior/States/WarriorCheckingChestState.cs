using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCheckingChestState : EnemyState<Warrior> {


    public override void OnStateUpdate(Warrior owner) {
        //Do some kind of animation or icon on the enemys head to show that he spotted a chest!
        //After the animation is over, move the enemy to the chest 
        owner.movementHandler.WalkToSelectedChest();

        if (owner.movementHandler.targetReached) {
            //Start checking the chest
        }
    }

    public override void OnStateEnter(Warrior owner) {
        Debug.Log("Entered Checking State");

        if (!owner.observationHandler.DefineTargetChest()) {

            Debug.Log("No chests in this room. Going to next room");
            owner.observationHandler.DefineTargetRoom();
            owner.movementHandler.DefineTarget(owner.observationHandler.targetRoom.transform);

            owner.stateMachine.SetState(new WarriorWanderState());
        } else {
            Debug.Log("There are chests here!");
            owner.observationHandler.DefineTargetChest();

            //Check if it's a normal chest or the enemy to grab the correct target position to walk to
            if (owner.observationHandler.targetChest.tag == "Chest") {
                owner.movementHandler.DefineTarget(owner.observationHandler.targetChest.GetComponent<Chest>().arrivalPoint);
            } else {

            }
            
        }
    }

    public override void OnStateExit(Warrior owner) {

    }


}
