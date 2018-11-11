using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomSenseRadius : MonoBehaviour {

    private PlayerManager playerManager;

    private void Start() {
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Room") {
            playerManager.currentRoom = other.GetComponent<Room>();
        }

    }
}
