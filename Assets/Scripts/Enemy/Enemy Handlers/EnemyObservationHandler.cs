using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObservationHandler : MonoBehaviour {

    public Room targetRoom;

    public TargetChestInfo targetChestInfo;

    public bool isWaitingForChestToOpen;

    private Enemy enemy;

    private void Awake() {
        enemy = GetComponent<Enemy>();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	public void DefineTargetRoom() {

        List<Room> possibleRooms = targetRoom.GetOpenRooms();

        int randomInt = Random.Range(0, possibleRooms.Count);

        targetRoom = possibleRooms[randomInt];

    }

    public bool DefineTargetChest() {

        List<GameObject> chestsInRoom = targetRoom.GetActiveChests();
        PlayerDirection direction;

        if(chestsInRoom.Count == 0) {
            return false;
        }

        int randomInt = Random.Range(0, chestsInRoom.Count);

        if (chestsInRoom[randomInt].tag == "Chest") {
            direction = chestsInRoom[randomInt].GetComponent<Chest>().chestDirection;
        } else {
            direction = chestsInRoom[randomInt].GetComponent<PlayerMovement>().playerDirection;
        }

        targetChestInfo = new TargetChestInfo(chestsInRoom[randomInt], direction);

        return true;
    }
}

[System.Serializable]
public struct TargetChestInfo {
    public GameObject targetChest;
    public PlayerDirection chestDirection;

    public TargetChestInfo(GameObject _targetChest, PlayerDirection _chestDirection) {
        targetChest = _targetChest;
        chestDirection = _chestDirection;
    }
}
