using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;


[CreateAssetMenu(fileName = "Player Stats",menuName = "Player Handlers/ Player Stat Handler")]
public class PlayerStatsHandler: ScriptableObject {

    public ChestType currentChestType;
    public int maxHealth = 3;
    public int currentHealth;

    public bool isHiding;
    public bool isDead;

    public GameObject currentChestPosessed;

    public Action OnChestTypeChange;
    public Action OnHealthChange;

    public void Initialize() {
        currentHealth = maxHealth;
        currentChestType = ChestType.Normal;
        currentChestPosessed = null;
        isDead = false;
        isHiding = false;
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
        CameraShaker.Instance.ShakeOnce(0.5f, 2f, .1f, 0.5f);

        if (currentHealth <= 0) {
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
