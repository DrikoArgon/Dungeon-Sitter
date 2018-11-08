using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementHandler), typeof(EnemyBattleHandler), typeof(EnemyStatsHandler))]
[RequireComponent(typeof(EnemyObservationHandler))]
public class Enemy : MonoBehaviour {

    public EnemyStats enemyStats;

    public EnemyStatsHandler statsHandler;
    public EnemyAnimationHandler animationHandler;
    public EnemyMovementHandler movementHandler;
    public EnemyObservationHandler observationHandler;
    public EnemyBattleHandler battleHandler;

    private void Awake() {
        statsHandler = GetComponent<EnemyStatsHandler>();
        animationHandler = GetComponent<EnemyAnimationHandler>();
        movementHandler = GetComponent<EnemyMovementHandler>();
        observationHandler = GetComponent<EnemyObservationHandler>();
        battleHandler = GetComponent<EnemyBattleHandler>();
    }
}
