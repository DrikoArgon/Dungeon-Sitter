using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {


    public List<RoomInfo> conectedRooms;
    public List<GameObject> chests;
    public Transform roomArrivalPoint;

	// Use this for initialization
	void Start () {
		
	}

    public void AddPlayerToList(GameObject player) {
        chests.Add(player);
    }

    public void RemovePlayerFromList() {
        chests.RemoveAt(chests.Count - 1);
    }

    public Transform GetArrivalPoint() {

        if(roomArrivalPoint != null) {
            return roomArrivalPoint;
        } else {
            return transform;
        }
    }
	
    public List<Room> GetOpenRooms() {

        List<Room> openRooms = new List<Room>();

        for (int i = 0; i < conectedRooms.Count; i++) {
            if(conectedRooms.Count > 1) {
                if (!conectedRooms[i].isNotAccessible && conectedRooms[i].room.gameObject.GetInstanceID() != gameObject.GetInstanceID()) {
                    openRooms.Add(conectedRooms[i].room);
                }
            } else {
                if (!conectedRooms[i].isNotAccessible) {
                    openRooms.Add(conectedRooms[i].room);
                }
            }
            
        }

        return openRooms;
    }

    public List<GameObject> GetActiveChests() {

        List<GameObject> activeChests = new List<GameObject>();

        for (int i = 0; i < chests.Count; i++) {
           
            if(chests[i].tag == "Chest") {
                if (!chests[i].GetComponent<Chest>().isDisabled) {
                    activeChests.Add(chests[i]);
                }
            } else {
                activeChests.Add(chests[i]);
            } 

        }

        return activeChests;

    }

}

[System.Serializable]
public struct RoomInfo {
    public Room room;
    public bool isNotAccessible;
}