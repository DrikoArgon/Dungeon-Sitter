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
        navmeshAgent.SetDestination(target.position);
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


    //public void WanderToTarget() {
    //    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
    //    DefineDirection();

    //    //enemyAnimationHandler.PlayWalkingAnimation(playerDirection);

    //    if (Vector2.Distance(enemy.transform.position, currentTarget.position) < 0.01f) {
    //        targetReached = true;
    //        //enemyAnimationHandler.PlayAnimation("Idle");
    //    }

    //}

    //public void WalkToSelectedChest(PlayerDirection chestDirection) {

    //    if(chestDirection == PlayerDirection.Right || chestDirection == PlayerDirection.Left) {
    //        MovementPriorityY();
    //    } else {
    //        MovementPriorityX();
    //    }

    //    DefineDirection();

    //    if (Vector2.Distance(enemy.transform.position, currentTarget.position) < 0.01f) {
    //        targetReached = true;
    //        //enemyAnimationHandler.PlayAnimation("Idle");
    //    }
    //}

    //public void ReturnToRoomArrivalPoint() {

    //    MovementPriorityX();

    //    DefineDirection();

    //    if (Vector2.Distance(enemy.transform.position, currentTarget.position) < 0.01f) {
    //        targetReached = true;
    //        //enemyAnimationHandler.PlayAnimation("Idle");
    //    }
    //}



    //To make sure the enemy will arrive from below or above
    //void MovementPriorityX() {

    //    if (currentTarget.position.x > enemy.transform.position.x && Mathf.Abs(currentTarget.position.x - enemy.transform.position.x) > 0.01f) {
    //        //Move Right
    //        enemy.transform.position += Vector3.right * currentSpeed * Time.deltaTime;
    //    } else if (currentTarget.position.x < enemy.transform.position.x && Mathf.Abs(currentTarget.position.x - enemy.transform.position.x) > 0.01f) {
    //        //Move left
    //        enemy.transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    //    } else if (currentTarget.position.y > enemy.transform.position.y && Mathf.Abs(currentTarget.position.y - enemy.transform.position.y) > 0.01f) {
    //        //Move Up
    //        enemy.transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    //    } else if (currentTarget.position.y < enemy.transform.position.y && Mathf.Abs(currentTarget.position.y - enemy.transform.position.y) > 0.01f) {
    //        //Move down
    //        enemy.transform.position += Vector3.down * currentSpeed * Time.deltaTime;
    //    }
    //}

    ////To make sure the enemy will arrive from the left or right sides
    //void MovementPriorityY() {

    //    if (currentTarget.position.y > enemy.transform.position.y && Mathf.Abs(currentTarget.position.y - enemy.transform.position.y) > 0.01f) {
    //        //Move Up
    //        enemy.transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    //    } else if (currentTarget.position.y < enemy.transform.position.y && Mathf.Abs(currentTarget.position.y - enemy.transform.position.y) > 0.01f) {
    //        //Move Down
    //        enemy.transform.position += Vector3.down * currentSpeed * Time.deltaTime;
    //    } else if (currentTarget.position.x > enemy.transform.position.x && Mathf.Abs(currentTarget.position.x - enemy.transform.position.x) > 0.01f) {
    //        //Move Right
    //        enemy.transform.position += Vector3.right * currentSpeed * Time.deltaTime;
    //    } else if (currentTarget.position.x < enemy.transform.position.x && Mathf.Abs(currentTarget.position.x - enemy.transform.position.x) > 0.01f) {
    //        //Move Left
    //        enemy.transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    //    }
    //}

    //public void ChaseTarget() {
    //    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
    //    DefineDirection();

    //    //enemyAnimationHandler.PlayRunningAnimation(playerDirection);
    //}

}
