using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

    public Image[] hearts;

    private int maxHealth;

    public Color32 deactivatedColor;

	// Use this for initialization
	void Start () {

        maxHealth = PlayerManager.instance.playerStatsHandler.maxHealth;

        PlayerManager.instance.playerStatsHandler.OnHealthChange += UpdateUI;

	}
	
	void UpdateUI() {

        int currentHealth = PlayerManager.instance.playerStatsHandler.currentHealth;

        for (int i = 0; i < maxHealth; i++) {

            if(i < currentHealth) {
                ActivateHeart(hearts[i]);
            } else {
                DeactivateHeart(hearts[i]);
            }
        }
    }

    void ActivateHeart(Image heart) {
        heart.color = Color.white;
    }

    void DeactivateHeart(Image heart) {
        
        heart.color = deactivatedColor;
    }
    
}
