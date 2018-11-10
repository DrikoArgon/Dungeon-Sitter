using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshAgentFollow : MonoBehaviour {

    public GameObject navmeshAgentObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = navmeshAgentObject.transform.position;
	}
}
