using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[CreateAssetMenu(fileName = "Player Attack Handler", menuName = "Player Handlers/ Player Attack Handler")]
public class PlayerAttackHandler: ScriptableObject {

    public bool isCompletelyCharged;
    public bool isAttacking;
    public bool isCharging;

    public float chargeAttackTime;
    public int chargedAttackCost = 5;

    public GameObject chargedProjectilePrefab;

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
            //DungeonManager.instance.dungeonTreasureManager.DecreaseAmountOfTreasure(chargedAttackCost);
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
        float animationPercentage = 0;
        bool hasSpawnedProjectile = false;

        while (elapsedTime < chargedAttackAnimationLength) {

            elapsedTime += Time.deltaTime;
            animationPercentage = elapsedTime / chargedAttackAnimationLength;

            if (!hasSpawnedProjectile) {
                if (animationPercentage > 0.367f) {

                    PlayerManager.instance.ShakeCamera(2f, 2f, 0.2f);
                    Instantiate(chargedProjectilePrefab, PlayerManager.instance.transform.position , Quaternion.identity);
                    hasSpawnedProjectile = true;
                }
            }
            

            yield return null;
        }

        AttackFinished();
    }
}
