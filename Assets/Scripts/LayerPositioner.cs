using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerPositioner : MonoBehaviour {

    public Transform pivotPoint;

	// Update is called once per frame
	void Update () {
        if (pivotPoint != null) {
            transform.position = new Vector3(transform.position.x, transform.position.y, pivotPoint.position.y * 0.5f);
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.5f);
        }
	}
}
