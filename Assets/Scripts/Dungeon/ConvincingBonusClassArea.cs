using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvincingBonusClassArea : MonoBehaviour {

    public AreaType areaType;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            AddBuff(collision.GetComponent<PlayerManager>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            RemoveBuff(collision.GetComponent<PlayerManager>());
        }
    }

    void AddBuff(PlayerManager player) {

        player.gameObject.AddComponent<ClassConvincingBuff>();
        player.gameObject.GetComponent<ClassConvincingBuff>().areaType = areaType;

    }

    void RemoveBuff(PlayerManager player) {
        Destroy(player.gameObject.GetComponent<ClassConvincingBuff>());
    }

}
public enum AreaType {
    Armory,
    Library,
    Nature,
    Holy
}
