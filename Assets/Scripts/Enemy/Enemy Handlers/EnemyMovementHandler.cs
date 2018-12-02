using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementHandler : MonoBehaviour {

    public NavMeshAgent navmeshAgent;

    public float currentSpeed;

    public Transform currentTarget;

    private Enemy enemy;
    public bool targetReached;

    public Vector2 enemyDirection;

    private EnemyAnimationHandler enemyAnimationHandler;

    // Use this for initialization

    private void Awake() {

        enemy = GetComponent<Enemy>();
        enemyAnimationHandler = enemy.GetComponent<EnemyAnimationHandler>();
    }

    void Start () {

	}

    public void StartChasing() {
        currentSpeed = enemy.enemyStats.chasingSpeed;
    }

    public void StartWandering() {

        currentSpeed = enemy.enemyStats.walkingSpeed;
    }

    public void DefineTarget(Transform target) {
        currentTarget = target;
        targetReached = false;
        navmeshAgent.SetDestination(new Vector3(target.position.x, target.position.y, 0));
    }


    public void ProcessMovement(bool isRunning) {

        DefineDirection();

        //if (isRunning) {
        //    enemyAnimationHandler.PlayWalkingAnimation(enemyDirection);
        //} else {
        //    enemyAnimationHandler.PlayRunningAnimation(enemyDirection);
        //}

        if (Vector2.Distance(enemy.transform.position, currentTarget.position) < 0.01f) {
            targetReached = true;
            //enemyAnimationHandler.PlayAnimation("Idle");
        }
    }

    public void LookAtChest(PlayerDirection chestDirection) {

        if (chestDirection == PlayerDirection.Right) {
            enemyDirection = new Vector2(-1, 0);
        } else if (chestDirection == PlayerDirection.Left) {
            enemyDirection = new Vector2(1, 0);
        } else if (chestDirection == PlayerDirection.Down) {
            enemyDirection = new Vector2(0, 1);
        } else {
            enemyDirection = new Vector2(0, -1);
        }

        //enemyAnimationHandler.PlayAnimation("Idle");
    }

    public void SetSpeed(float speed) {
        currentSpeed = speed;
    }

    void DefineDirection() {

        if (enemy.transform.position.x < currentTarget.position.x) {
            enemyDirection.x = 1;
        } else {
            enemyDirection.x = -1;
        }

        if (enemy.transform.position.y < currentTarget.position.y) {
            enemyDirection.y = 1;
        } else {
            enemyDirection.y = -1;
        }
    }
}
