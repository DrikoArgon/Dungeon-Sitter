using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private float currentSpeed;

    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode moveUpKey;
    public KeyCode moveDownKey;

    private bool movingUp;
    private bool movingDown;
    private bool movingLeft;
    private bool movingRight;

    private bool diagonal;

    private Rigidbody2D myRigidBody;

    public PlayerDirection playerDirection = PlayerDirection.Down;
    public bool walking;
    public bool idle;

    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();

        currentSpeed = speed;
    }

    // Update is called once per frame
    // Inputs should be registered here
    void Update() {
        
        if (!PlayerManager.instance.CanMove()) {
            return;
        }

        if (Input.GetKey(moveLeftKey)) {
            movingLeft = true;
            movingRight = false;
        } else {
            movingLeft = false;
        } 

        if (Input.GetKey(moveRightKey)) {
            movingRight = true;
            movingLeft = false;
        } else {
            movingRight = false;
        }

        if (Input.GetKey(moveUpKey)) {
            movingUp = true;
            movingDown = false;
        } else {
            movingUp = false;
        }

        if (Input.GetKey(moveDownKey)) {
            movingDown = true;
            movingUp = false;
        } else {
            movingDown = false;
        }

        if (movingRight && (movingUp || movingDown)) { //The player is moving diagonaly
            diagonal = true;
        } else if (movingLeft && (movingUp || movingDown)) { //The player is moving diagonaly
            diagonal = true;
        } else {
            diagonal = false;
        }


    }

    //Movement should occur here
    void FixedUpdate() {

        if (!PlayerManager.instance.CanMove()) {
            if (movingLeft && movingRight && movingUp && movingDown) {
                Idle();
            }
            return;
        }

        if (movingUp) {
            MoveUp();
        }

        if (movingDown) {
            MoveDown();
        }

        if (movingLeft) {
            MoveLeft();
        }

        if (movingRight) {
            MoveRight();
        }



        if (!movingLeft && !movingRight && !movingUp && !movingDown) {
            Idle();
        }
    }

    void MoveLeft() {

        if (!walking) {
            idle = false;
            walking = true;
        }

        PlayerManager.instance.playerAnimationHandler.PlayWalkingAnimation(-1, 0);

        myRigidBody.transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (playerDirection != PlayerDirection.Left) {

            playerDirection = PlayerDirection.Left;

        }


    }

    void MoveRight() {

        if (!walking) {
            idle = false;
            walking = true;
        }

        PlayerManager.instance.playerAnimationHandler.PlayWalkingAnimation(1, 0);

        myRigidBody.transform.position += Vector3.right * currentSpeed * Time.deltaTime;

        if (playerDirection != PlayerDirection.Right) {

            playerDirection = PlayerDirection.Right;

        }
    }

    void MoveUp() {

        if (!walking) {
            idle = false;
            walking = true;
        }

        PlayerManager.instance.playerAnimationHandler.PlayWalkingAnimation(0, 1);

        myRigidBody.transform.position += Vector3.up * currentSpeed * Time.deltaTime;

        if (playerDirection != PlayerDirection.Up && !diagonal) {

            playerDirection = PlayerDirection.Up;

        }
    }

    void MoveDown() {

        if (!walking) {
            walking = true;
            idle = false;
        }

        PlayerManager.instance.playerAnimationHandler.PlayWalkingAnimation(0, -1);

        myRigidBody.transform.position += Vector3.down * currentSpeed * Time.deltaTime;

        if (playerDirection != PlayerDirection.Down && !diagonal) {

            playerDirection = PlayerDirection.Down;

        }
    }

    void Idle() {

        if (!idle) {
            PlayerManager.instance.playerAnimationHandler.PlayIdleAnimation();
            idle = true;
            walking = false;
        }

        movingLeft = false;
        movingRight = false;
        movingUp = false;
        movingDown = false;


    }

    void PrepareForJumpSpeed() {
        currentSpeed = 0;
    }

    void JumpSpeed() {
        if (diagonal) {
            currentSpeed = speed / 1.4f;
        } else {
            currentSpeed = speed;
        }
    }
}

public enum PlayerDirection {
    Up,
    Down,
    Left,
    Right
}
