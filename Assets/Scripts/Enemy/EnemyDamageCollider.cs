using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour {

    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            PlayerManager.instance.playerStatsHandler.TakeDamage(damage);
            
        }
    }
}
