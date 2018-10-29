using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour {

    public int currentTreasureAmount;
    public int currentHealth;

    private Enemy enemy;
    // Use this for initialization
    void Start () {

        enemy = GetComponent<Enemy>();

        currentHealth = enemy.enemyStats.maxHealth;
        currentTreasureAmount = enemy.enemyStats.initialTreasureAmount;
	}
	
}
