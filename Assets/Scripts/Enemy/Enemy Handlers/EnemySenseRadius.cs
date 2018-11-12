using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenseRadius : MonoBehaviour {

    Enemy enemy;
    EnemyStatsHandler enemyStatsHandler;
    EnemyBattleHandler enemyBattleHandler;

    private void Start() {
        enemy = GetComponentInParent<Enemy>();
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if (!PlayerManager.instance.playerStatsHandler.isHiding) {
                enemy.battleHandler.target = other.gameObject;
                enemy.statsHandler.isPlayerSensed = true;
            } 
        }
    }
}
