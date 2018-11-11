using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour {

    public int currentTreasureAmount;
    public int currentHealth;

    public bool isPlayerSensed;
    public bool isAttacking;
    public bool isHurt;
    public bool isDead;

    private Enemy enemy;
    // Use this for initialization
    void Start () {

        enemy = GetComponent<Enemy>();

        currentHealth = enemy.enemyStats.maxHealth;
        currentTreasureAmount = enemy.enemyStats.initialTreasureAmount;

	}

    public void IncreaseCurrentTreasure(int amount) {
        currentTreasureAmount += amount;
    }

    public void ReceiveDamage(int damage) {

        isHurt = true;
        currentHealth -= damage;

        enemy.GetComponent<EnemyAnimationHandler>().PlayAnimation("Hurt");

        if (currentHealth <= 0) {
            currentHealth = 0;

            isDead = true;

        }

    }

	
}
