using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager instance;

    public DungeonTreasureManager dungeonTreasureManager;

    public MeshRenderer planeMeshRenderer;
    public NavMeshSurface navmeshSurface;

    void Awake() {
        if (DungeonManager.instance == null) {
            instance = this;
            dungeonTreasureManager.Initialize();
            planeMeshRenderer.enabled = true;

            //navmeshSurface.BuildNavMesh();

            planeMeshRenderer.enabled = false;
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
}
