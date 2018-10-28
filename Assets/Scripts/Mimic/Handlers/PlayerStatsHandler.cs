using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Player Stats",menuName = "Player Handlers/ Player Stat Handler")]
public class PlayerStatsHandler: ScriptableObject {

    public ChestType currentChestType;
    public int maxHealth = 3;
    public int currentHealth;

    public bool isDead;

    public GameObject currentChestPosessed;

    public Action OnChestTypeChange;
    public Action OnHealthChange;

    public void Initialize() {
        currentHealth = maxHealth;
        currentChestType = ChestType.Normal;
        currentChestPosessed = null;
    }

    public void SetChest(GameObject newChest) {

        currentChestPosessed = newChest;
        Chest chest = newChest.GetComponent<Chest>();
        
        if (chest == null) {
            Debug.LogError("This object has no Chest component. Adding it .");
            newChest.AddComponent<Chest>();

            chest = newChest.GetComponent<Chest>();
        } 

        if(chest.chestType != currentChestType) {
            SetChestType(chest.chestType);
        }
    }

    public void SetChestType(ChestType type) {
        currentChestType = type;

        if(OnChestTypeChange != null) {
            OnChestTypeChange();
        }
    }

    public void TakeDamage(int damage) {

        currentHealth -= damage; 

        if(currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }

        if (OnHealthChange != null) {
            OnHealthChange();
        }

    }

    public void Die() {
        isDead = true;
    }

}

public enum ChestType {
    Normal,
    Magic,
    Hunter,
    Warrior
}
