using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {


    public List<RoomInfo> conectedRooms;
    public List<GameObject> chests;

	// Use this for initialization
	void Start () {
		
	}
	
    public List<Room> GetOpenRooms(Room lastRoom) {

        List<Room> openRooms = new List<Room>();

        for (int i = 0; i < conectedRooms.Count; i++) {
            if(conectedRooms.Count > 1) {
                if (!conectedRooms[i].isNotAccessible && conectedRooms[i].room.gameObject.GetInstanceID() != lastRoom.gameObject.GetInstanceID()) {
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

}

[System.Serializable]
public struct RoomInfo {
    public Room room;
    public bool isNotAccessible;
}