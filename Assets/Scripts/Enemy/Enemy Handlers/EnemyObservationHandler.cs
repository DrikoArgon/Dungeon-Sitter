using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObservationHandler : MonoBehaviour {

    public Room targetRoom;

    public GameObject targetChest;

    private Enemy enemy;

    private void Awake() {
        enemy = GetComponent<Enemy>();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	public void DefineTargetRoom() {

        List<Room> possibleRooms = targetRoom.GetOpenRooms(targetRoom);

        int randomInt = Random.Range(0, possibleRooms.Count);

        targetRoom = possibleRooms[randomInt];

    }

    public bool DefineTargetChest() {

        List<GameObject> chestsInRoom = targetRoom.chests;

        if(chestsInRoom.Count == 0) {
            return false;
        }

        int randomInt = Random.Range(0, chestsInRoom.Count);

        targetChest = chestsInRoom[randomInt];

        Debug.Log("Target chest name: " + targetChest);
        return true;
    }
}
