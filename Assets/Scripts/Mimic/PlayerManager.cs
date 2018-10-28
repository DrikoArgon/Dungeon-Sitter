using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    public PlayerAnimationHandler playerAnimationHandler;
    public PlayerStatsHandler playerStatsHandler;
    public PlayerAttackHandler playerAttackHandler;
    public PlayerChestChangeHandler playerChestChangeHandler;

    void Awake() {
        if(PlayerManager.instance == null) {
            instance = this;

            playerAnimationHandler.Initialize();
            playerStatsHandler.Initialize();
            playerAttackHandler.Initialize();
            playerChestChangeHandler.Initialize();
        }
    }

    public bool CanMove() {
        if (GameManager.instance.uiActive || playerAttackHandler.isAttacking || playerAttackHandler.isCharging || playerStatsHandler.isDead || playerChestChangeHandler.isTransfering) {
            return false;
        } else {
            return true;
        }
    }


    public bool CanAttack() {
        if (GameManager.instance.uiActive || playerAttackHandler.isAttacking || playerAttackHandler.isCharging || playerStatsHandler.isDead || playerChestChangeHandler.isTransfering) {
            return false;
        } else {
            return true;
        }
    }
}
