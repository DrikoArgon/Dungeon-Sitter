using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerInteractionControls : MonoBehaviour {

	public static PlayerInteractionControls instance;

    public bool isRightButtonPressed;

	void Awake(){
        if (PlayerInteractionControls.instance == null) {
            instance = this;
        }
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(GameManager.instance.uiActive){
			return;
		}

        if (!isRightButtonPressed) {

            if (Input.GetMouseButtonDown(0) && PlayerManager.instance.CanAttack()) {
                PlayerManager.instance.playerAttackHandler.Attack();
            }

            if (Input.GetMouseButtonDown(1) && !isRightButtonPressed && PlayerManager.instance.CanAttack() && DungeonManager.instance.dungeonTreasureManager.currentTreasureInDungeon >= PlayerManager.instance.playerAttackHandler.chargedAttackCost) {
                isRightButtonPressed = true;
                PlayerManager.instance.playerAttackHandler.StartChargingAttack();
            }

            if (Input.GetKeyDown(KeyCode.E) && PlayerManager.instance.CanAttack()) {
                PlayerManager.instance.playerChestChangeHandler.TransferToChest();
            }

            if (Input.GetKeyDown(KeyCode.Space)) {

                if (!PlayerManager.instance.playerStatsHandler.isHiding) {
                    PlayerManager.instance.HidePlayer(true);
                } else {
                    PlayerManager.instance.HidePlayer(false);
                }

            }

        } else {

            if (Input.GetMouseButtonUp(1) && isRightButtonPressed && PlayerManager.instance.playerAttackHandler.isCharging) {
                isRightButtonPressed = false;
                PlayerManager.instance.playerAttackHandler.UnleashChargedAttack();
            }

        }
	}

}
