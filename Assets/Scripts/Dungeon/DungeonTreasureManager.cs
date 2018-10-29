using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Dungeon Treasure Handler", menuName = "Dungeon Handlers/Dungeon Treasure Handler")]
public class DungeonTreasureManager : ScriptableObject {

    public int currentTreasureInDungeon;

    public Chest[] dungeonChests;

    public Action OnTreasureAmountChange;


    public void Initialize() {

        currentTreasureInDungeon = 0;
        dungeonChests = FindObjectsOfType<Chest>();
        AccountAmountOfTreasureInDungeon();
    }

    void AccountAmountOfTreasureInDungeon() {

        foreach(Chest chest in dungeonChests) {
            currentTreasureInDungeon += chest.treasureAmount;
        }

    }

    public void IncreaseAmountOfTreasure(int amount) {

        currentTreasureInDungeon += amount;

        if(currentTreasureInDungeon > 999) {
            currentTreasureInDungeon = 999;
        }

        if(OnTreasureAmountChange != null) {
            OnTreasureAmountChange();
        }
    }

    public void DecreaseAmountOfTreasure(int amount) {
        currentTreasureInDungeon -= amount;

        if(currentTreasureInDungeon < 0) {
            currentTreasureInDungeon = 0;
        }

        if (OnTreasureAmountChange != null) {
            OnTreasureAmountChange();
        }
    }
}
