using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ChargedAttackProjectile : MonoBehaviour {

    public float projectileSpeed = 6f;
    public float timeToDisappear = 3f;
    public int damage;

    public PlayerDirection directionToGo;

    private Rigidbody2D myRigidBody;
	// Use this for initialization
	void Start () {
        directionToGo = PlayerManager.instance.playerMovementHandler.playerDirection;
        myRigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDisappear);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate() {
        switch (directionToGo) {
            case PlayerDirection.Up:
                myRigidBody.position += Vector2.up * projectileSpeed * Time.deltaTime; 
                break;
            case PlayerDirection.Down:
                myRigidBody.position += Vector2.down * projectileSpeed * Time.deltaTime;
                break;
            case PlayerDirection.Left:
                myRigidBody.position += Vector2.left * projectileSpeed * Time.deltaTime;
                break;
            case PlayerDirection.Right:
                myRigidBody.position += Vector2.right * projectileSpeed * Time.deltaTime;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
