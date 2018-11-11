using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerChestSenseCollider : MonoBehaviour {

    private Transform playerTransform;

    public List<GameObject> chestsInsideRadius;

    void Start() {
        playerTransform = transform.parent;
    }

    void Update() {
        if (chestsInsideRadius.Count > 0) {
            SortChestListByDistance();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Chest") {
            if(!other.GetComponent<Chest>().isTargeted && !other.GetComponent<Chest>().isDisabled) {
                chestsInsideRadius.Add(other.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "Chest") {
            for (int i = 0; i < chestsInsideRadius.Count; i++) {
                if (other.gameObject.GetInstanceID() == chestsInsideRadius[i].GetInstanceID()) {

                    chestsInsideRadius.RemoveAt(i);

                    break;
                }
            }
        }
    }

    public void RemoveCurrentFromList() {

        for (int i = 0; i < chestsInsideRadius.Count; i++) {
            if (PlayerManager.instance.playerStatsHandler.currentChestPosessed.GetInstanceID() == chestsInsideRadius[i].GetInstanceID()) {

                chestsInsideRadius.RemoveAt(i);
                break;
            }
        }
    }

    void SortChestListByDistance() {

        chestsInsideRadius.Sort((p1, p2) => Vector2.Distance(playerTransform.transform.position, p1.transform.position).CompareTo(Vector2.Distance(playerTransform.transform.position, p2.transform.position)));

    }
}
