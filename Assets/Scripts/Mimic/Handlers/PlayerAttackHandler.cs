using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Attack Handler", menuName = "Player Handlers/ Player Attack Handler")]
public class PlayerAttackHandler: ScriptableObject {

    public bool isCompletelyCharged;
    public bool isAttacking;
    public bool isCharging;

    public float chargeAttackTime;

    private float fullChargeAnimationLength;
    private float chargedAttackAnimationLength;

    public PlayerAttack normalAttack;

    public PlayerAttack currentAttack;

    private Coroutine chargeCoroutine;
    private Dictionary<ChestType, PlayerAttack> attacksList;

    public void Initialize() {

        isAttacking = false;
        isCharging = false;
        isCompletelyCharged = false;

        attacksList = new Dictionary<ChestType, PlayerAttack>();

        attacksList.Add(ChestType.Normal, normalAttack);

        fullChargeAnimationLength = PlayerManager.instance.playerAnimationHandler.GetAnimationLength("FullCharge");
        chargedAttackAnimationLength = PlayerManager.instance.playerAnimationHandler.GetAnimationLength("ChargedAttack");

        currentAttack = normalAttack;

        PlayerManager.instance.playerStatsHandler.OnChestTypeChange += UpdateAttackData;
    }

    void UpdateAttackData() {
        currentAttack = attacksList[PlayerManager.instance.playerStatsHandler.currentChestType];
    }

    public void Attack() {

        PlayerManager.instance.playerAnimationHandler.PlayAttackAnimation();
        isAttacking = true;
        PlayerManager.instance.StartCoroutine(ProcessAttack());
        
    }

    public void AttackFinished() {
        isAttacking = false;
        PlayerManager.instance.playerAnimationHandler.PlayIdleAnimation();
    }

    public void StartChargingAttack() {
        PlayerManager.instance.playerAnimationHandler.PlayChargingAnimation();
        chargeCoroutine = PlayerManager.instance.StartCoroutine(ChargeAttack());
    }

    public void UnleashChargedAttack() {

        isCharging = false;

        if (isCompletelyCharged) {

            isAttacking = true;

            PlayerManager.instance.playerAnimationHandler.PlayChargedAttackAnimation();
            PlayerManager.instance.StartCoroutine(ProcessChargedAttack());

        } else {
            PlayerManager.instance.playerAnimationHandler.PlayIdleAnimation();
        } 

        if (chargeCoroutine != null) {
            PlayerManager.instance.StopCoroutine(chargeCoroutine);
            chargeCoroutine = null;
        }
       
        isCompletelyCharged = false;
    }

    IEnumerator ChargeAttack() {

        isCharging = true;

        float elapsedTime = 0;

        while (elapsedTime < chargeAttackTime) {
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0;

        PlayerManager.instance.playerAnimationHandler.PlayFullChargeAnimation();
        isCompletelyCharged = true;

        while (elapsedTime < fullChargeAnimationLength) {

            elapsedTime += Time.deltaTime;

            yield return null;
        }


        PlayerManager.instance.playerAnimationHandler.PlayChargedAnimation();

    }

    IEnumerator ProcessAttack() {

        currentAttack.StartAttack(PlayerManager.instance.playerAnimationHandler.GetAttackAnimationLength());

        while (isAttacking) {
            currentAttack.ProcessAttack();
            yield return null;
        }

    }

    IEnumerator ProcessChargedAttack() {

        float elapsedTime = 0;

        while (elapsedTime < chargedAttackAnimationLength) {

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        AttackFinished();
    }
}
