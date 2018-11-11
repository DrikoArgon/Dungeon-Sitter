﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    public PlayerAnimationHandler playerAnimationHandler;
    public PlayerStatsHandler playerStatsHandler;
    public PlayerAttackHandler playerAttackHandler;
    public PlayerChestChangeHandler playerChestChangeHandler;

    [HideInInspector]
    public PlayerMovement playerMovementHandler;

    public Transform playerArrivalPointUp;
    public Transform playerArrivalPointDown;
    public Transform playerArrivalPointLeft;
    public Transform playerArrivalPointRight;

    public Room currentRoom;

    public Enemy enemyOpeningChest;

    public int convincingLevel;

    void Awake() {
        if(PlayerManager.instance == null) {
            instance = this;

            playerAnimationHandler.Initialize();
            playerStatsHandler.Initialize();
            playerAttackHandler.Initialize();
            playerChestChangeHandler.Initialize();
            playerMovementHandler = GetComponent<PlayerMovement>();
            convincingLevel = 0;
        }
    }

    public Transform GetPlayerArrivalPoint() {

        if(playerMovementHandler.playerDirection == PlayerDirection.Up) {
            return playerArrivalPointUp;
        }else if (playerMovementHandler.playerDirection == PlayerDirection.Down) {
            return playerArrivalPointDown;
        } else if (playerMovementHandler.playerDirection == PlayerDirection.Left) {
            return playerArrivalPointLeft;
        } else {
            return playerArrivalPointRight;
        }

    }

    public bool CanMove() {
        if (GameManager.instance.uiActive || playerAttackHandler.isAttacking || playerAttackHandler.isCharging || playerStatsHandler.isDead || playerChestChangeHandler.isTransfering || playerStatsHandler.isHiding) {
            return false;
        } else {
            return true;
        }
    }


    public bool CanAttack() {
        if (GameManager.instance.uiActive || playerAttackHandler.isAttacking || playerAttackHandler.isCharging || playerStatsHandler.isDead || playerChestChangeHandler.isTransfering || playerStatsHandler.isHiding) {
            return false;
        } else {
            return true;
        }
    }

    public void OpenChest(Enemy _enemyOpeningChest) {
        enemyOpeningChest = _enemyOpeningChest;

        StartCoroutine(ProcessOpenChest());

    }

    public void HidePlayer(bool _isHiding) {

        playerStatsHandler.isHiding = _isHiding;
        playerAnimationHandler.PlayAnimation("Hide");

        if(currentRoom != null) {
            if (_isHiding) {
                GetComponent<Rigidbody2D>().isKinematic = true;
                currentRoom.AddPlayerToList(this.gameObject);
            } else {
                GetComponent<Rigidbody2D>().isKinematic = false;
                currentRoom.RemovePlayerFromList();
            }
        }

    }

    public void EatEnemy() {
        DungeonManager.instance.dungeonTreasureManager.IncreaseAmountOfTreasure(enemyOpeningChest.statsHandler.currentTreasureAmount);
        enemyOpeningChest.statsHandler.Execute();
    }

    public void SpawnEatenEnemyObject() {
        //Spawn the item that flyes when you eat the enemy
        
    }

    IEnumerator ProcessOpenChest() {

        float elapsedTime = 0;
        float openChestAnimationLength = playerAnimationHandler.GetAnimationLength("TrueOpen");
        float executeAnimationLength = playerAnimationHandler.GetAnimationLength("Execute");

        playerAnimationHandler.PlayAnimation("TrueOpen");

        while (elapsedTime < openChestAnimationLength) {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        enemyOpeningChest.GetComponent<EnemyObservationHandler>().isWaitingForChestToOpen = false;

        elapsedTime = 0;
        playerAnimationHandler.PlayAnimation("Execute");

        while (elapsedTime < executeAnimationLength) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerAnimationHandler.PlayAnimation("Hide");
    }

}
