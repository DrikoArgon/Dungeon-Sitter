using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvincingBonusArea : MonoBehaviour {

    public List<ConvincingBonusObject> convincingBonusObjects;

    public int convincingTotalBonus;

    // Use this for initialization
    void Start () {
        convincingTotalBonus = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DetermineConvincingTotal() {    

        foreach(ConvincingBonusObject bonusObject in convincingBonusObjects) {
            convincingTotalBonus += bonusObject.convincingBonus;
        }
    }

    public void AddToConvincingTotal(int bonus) {
        convincingTotalBonus += bonus;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.tag == "Player") {
            collision.GetComponent<PlayerManager>().CalculateConvincingLevel(convincingTotalBonus);
        }

        if(collision.tag == "Memento") {
            AddToConvincingTotal(collision.GetComponent<ConvincingBonusObject>().convincingBonus);
            collision.GetComponent<PlayerManager>().CalculateConvincingLevel(convincingTotalBonus);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerManager>().ResetConvincingLevel();
        }
    }
}
