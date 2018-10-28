using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour {

    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Enemy") {

        }
    }
}
