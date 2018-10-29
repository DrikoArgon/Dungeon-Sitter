using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonTreasureUI : MonoBehaviour {

    public Text treasureAmountText;

    private int currentTreasure;

    private Coroutine updateCoroutine;

    void Start() {
        currentTreasure = DungeonManager.instance.dungeonTreasureManager.currentTreasureInDungeon;
        treasureAmountText.text = currentTreasure.ToString("000");
        DungeonManager.instance.dungeonTreasureManager.OnTreasureAmountChange += UpdateUI;
    }

    void UpdateUI() {

        if(DungeonManager.instance.dungeonTreasureManager.currentTreasureInDungeon < currentTreasure) {

            if(updateCoroutine != null) {
                StopCoroutine(updateCoroutine);
                updateCoroutine = null;
            }

            updateCoroutine = StartCoroutine(DecreaseAmount());

        } else {

            if (updateCoroutine != null) {
                StopCoroutine(updateCoroutine);
                updateCoroutine = null;
            }

            updateCoroutine = StartCoroutine(IncreaseAmount());

        }

    }

    IEnumerator DecreaseAmount() {

        float newAmount = DungeonManager.instance.dungeonTreasureManager.currentTreasureInDungeon;

        while (currentTreasure > newAmount) {
            currentTreasure--;
            treasureAmountText.text = currentTreasure.ToString("000");
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator IncreaseAmount() {

        float newAmount = DungeonManager.instance.dungeonTreasureManager.currentTreasureInDungeon;

        while(currentTreasure < newAmount) {
            currentTreasure++;
            treasureAmountText.text = currentTreasure.ToString("000");
            yield return new WaitForSeconds(0.1f);
        }

    }
}

