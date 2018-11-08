using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager instance;

    public DungeonTreasureManager dungeonTreasureManager;

    void Awake() {
        if (DungeonManager.instance == null) {
            instance = this;

            dungeonTreasureManager.Initialize();

        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
