using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour {

    public float currentSpeed;

    public Transform currentTarget;

    private Enemy enemy;
    public bool targetReached;

    public Vector2 playerDirection;

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
    }

    public void WanderToTarget() {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
        DefineDirection();

        //enemyAnimationHandler.PlayWalkingAnimation(playerDirection);

        if (Vector2.Distance(enemy.transform.position, currentTarget.position) < 0.01f) {
            targetReached = true;
            //enemyAnimationHandler.PlayAnimation("Idle");
        }

    }

    public void WalkToSelectedChest() {

        if(currentTarget.position.x > enemy.transform.position.x && Mathf.Abs(currentTarget.position.x - enemy.transform.position.x) > 0.01f) {
            //Move Right
            enemy.transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        } else if(currentTarget.position.x < enemy.transform.position.x && Mathf.Abs(currentTarget.position.x - enemy.transform.position.x) > 0.01f) {
            //Move left
            enemy.transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        } else if (currentTarget.position.y > enemy.transform.position.y && Mathf.Abs(currentTarget.position.y - enemy.transform.position.y) > 0.01f) {
            //Move Up
            enemy.transform.position += Vector3.up * currentSpeed * Time.deltaTime;
        } else if(currentTarget.position.y < enemy.transform.position.y && Mathf.Abs(currentTarget.position.y - enemy.transform.position.y) > 0.01f)  {
            //Move down
            enemy.transform.position += Vector3.down * currentSpeed * Time.deltaTime;
        }

        DefineDirection();

        if (Vector2.Distance(enemy.transform.position, currentTarget.position) < 0.01f) {
            targetReached = true;
            //enemyAnimationHandler.PlayAnimation("Idle");
        }
    }

    public void ChaseTarget() {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
        DefineDirection();

        //enemyAnimationHandler.PlayRunningAnimation(playerDirection);
    }

    public void SetSpeed(float speed) {
        currentSpeed = speed;
    }

    void DefineDirection() {
        if (enemy.transform.position.x < currentTarget.position.x) {
            playerDirection.x = 1;
        } else {
            playerDirection.x = -1;
        }

        if (enemy.transform.position.y < currentTarget.position.y) {
            playerDirection.y = 1;
        } else {
            playerDirection.y = -1;
        }
    }

}
