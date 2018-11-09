using System.Collections;
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
}
