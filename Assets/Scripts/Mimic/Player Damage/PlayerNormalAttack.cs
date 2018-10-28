using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Attacks/ Normal Attack")]
public class PlayerNormalAttack : PlayerAttack {

    public override void StartAttack(float animationLength) {
        attackTimer = 0;
        attackAnimationLength = animationLength;

    }

    public override void ProcessAttack() {

        attackTimer += Time.deltaTime;
        animationPercent = attackTimer / attackAnimationLength;

        if(animationPercent >= 1f) {
            FinishAttack();
        }
        
    }

    public override void InterruptAttack() {
        PlayerManager.instance.playerAttackHandler.AttackFinished();
    }

    public override void FinishAttack() {
        //Debug.Log("Normal Attack Finished");
        PlayerManager.instance.playerAttackHandler.AttackFinished();
    }
}
