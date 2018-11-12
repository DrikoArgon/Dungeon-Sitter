using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour {

    Enemy enemy;
    EnemyStatsHandler enemyStatsHandler;
    EnemyBattleHandler enemyBattleHandler;

    private void Start() {
        enemy = GetComponentInParent<Enemy>();
    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {

             enemy.statsHandler.isPlayerOnAttackRadius = true;

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            enemy.statsHandler.isPlayerOnAttackRadius = false;
        }
    }
}
