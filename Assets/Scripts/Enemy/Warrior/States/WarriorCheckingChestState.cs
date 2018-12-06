using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCheckingChestState : EnemyState<Warrior> {

    private bool isReturningToRoomArrivalPoint;
    private bool alreadyOpenedChest;

    public override void OnStateUpdate(Warrior owner) {
        //Do some kind of animation or icon on the enemys head to show that he spotted a chest!
        //After the animation is over, move the enemy to the chest 


        owner.movementHandler.ProcessMovement(false);

        if (owner.movementHandler.targetReached) {
            //Start checking the chest. 

            if (owner.observationHandler.targetChestInfo.targetChest.tag == "Chest") {

                //Look at the chest
                owner.movementHandler.LookAtChest(owner.observationHandler.targetChestInfo.chestDirection);

                //Open the chest
                if (!alreadyOpenedChest) {
                    owner.observationHandler.isWaitingForChestToOpen = true;
                    owner.observationHandler.targetChestInfo.targetChest.GetComponent<Chest>().OpenChest(owner);
                    alreadyOpenedChest = true;
                }

                if (!owner.observationHandler.isWaitingForChestToOpen) {

                    owner.statsHandler.IncreaseCurrentTreasure(owner.observationHandler.targetChestInfo.targetChest.GetComponent<Chest>().treasureAmount);
                    RestartSearch(owner);

                }

            } else {

                //Faz as coisinhas de desconfiança tudo pq eh o mimico
                owner.movementHandler.LookAtChest(owner.observationHandler.targetChestInfo.targetChest.GetComponent<PlayerMovement>().playerDirection);

                //Open the chest
                if (!alreadyOpenedChest) {
                    owner.observationHandler.isWaitingForChestToOpen = true;
                    owner.observationHandler.targetChestInfo.targetChest.GetComponent<PlayerManager>().OpenChest(owner);
                    alreadyOpenedChest = true;
                }

                if (!owner.observationHandler.isWaitingForChestToOpen) {

                    //Play surprised animation


                }
            }
        } else {

            if (owner.statsHandler.isPlayerSensed) {
                owner.stateMachine.SetState(new WarriorChasingState());
            }
        }
    }

    public override void OnStateEnter(Warrior owner) {
        alreadyOpenedChest = false;

        if (!owner.observationHandler.DefineTargetChest()) {

            owner.observationHandler.DefineTargetRoom();
            owner.movementHandler.DefineTarget(owner.observationHandler.targetRoom.GetArrivalPoint());

            owner.stateMachine.SetState(new WarriorWanderState());
        } else {

            owner.observationHandler.DefineTargetChest();

            //Check if it's a normal chest or the enemy to grab the correct target position to walk to
            if (owner.observationHandler.targetChestInfo.targetChest.tag == "Chest") {
                owner.movementHandler.DefineTarget(owner.observationHandler.targetChestInfo.targetChest.GetComponent<Chest>().arrivalPoint);
            } else {

                Transform target = owner.observationHandler.targetChestInfo.targetChest.GetComponent<PlayerManager>().GetPlayerArrivalPoint();
                owner.movementHandler.DefineTarget(target);
            }

        }
    }

    void RestartSearch(Warrior owner) {

        OnStateEnter(owner);
    }

    public override void OnStateExit(Warrior owner) {
        isReturningToRoomArrivalPoint = false;
    }


}
