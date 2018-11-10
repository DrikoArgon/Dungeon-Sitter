using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class NavMeshMovingTest : MonoBehaviour {

    bool canMove;
    public NavMeshAgent nav;

    // Use this for initialization
    void Start() {
        canMove = true;
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (!EventSystem.current.IsPointerOverGameObject() && canMove == true) {
                    nav.SetDestination(hit.point);
                }
            }
        }
    }
}
