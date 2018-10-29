using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour {

    public float currentSpeed;

    public bool isPlayerOnSenseRange;
    public bool isPlayerOnAttackRange;

    private Enemy enemy;
    // Use this for initialization
    void Start () {

        enemy = GetComponent<Enemy>();
        currentSpeed = GetComponent<Enemy>().enemyStats.walkingSpeed;
	}

}
