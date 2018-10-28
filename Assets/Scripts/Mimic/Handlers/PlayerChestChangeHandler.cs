using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Chest Change Handler", menuName = "Player Handlers/ Player Chest Change Handler")]
public class PlayerChestChangeHandler : ScriptableObject {

    public PlayerChestSenseCollider senseCollider;

    private float chestAbandonAnimationLength;
    private float chestPossessionAnimationLength;
    private Transform playerTransform;

    public float transferSpeed;
    public bool isTransfering;

    private GameObject targetChest;

    public void Initialize() {

        playerTransform = PlayerManager.instance.transform;
        senseCollider = PlayerManager.instance.gameObject.GetComponentInChildren<PlayerChestSenseCollider>();

        isTransfering = false;
        chestAbandonAnimationLength = PlayerManager.instance.playerAnimationHandler.GetAnimationLength("Abandon");
        chestPossessionAnimationLength = PlayerManager.instance.playerAnimationHandler.GetAnimationLength("Possess");
    }

    public void TransferToChest() {

        if(senseCollider.chestsInsideRadius.Count > 0) {

            playerTransform.GetComponent<Collider2D>().isTrigger = true;
            targetChest = senseCollider.chestsInsideRadius[0];
            PlayerManager.instance.StartCoroutine(GoToNewChestLocation());
        }

    }

    public void EndTransfer() {
        playerTransform.GetComponent<Collider2D>().isTrigger = false;
        isTransfering = false;
        senseCollider.RemoveCurrentFromList();
        PlayerManager.instance.playerAnimationHandler.PlayIdleAnimation();
    }

    IEnumerator GoToNewChestLocation() {

        Debug.Log("Target Chest is: " + targetChest.name);

        Vector2 targetLocation = targetChest.transform.position;

        Debug.Log("Target Location: " + targetLocation);

        isTransfering = true;
        float elapsedTime = 0;

        PlayerManager.instance.playerAnimationHandler.PlayAnimation("Abandon");

        while (elapsedTime < chestAbandonAnimationLength) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Float until target chest
        PlayerManager.instance.playerAnimationHandler.PlayAnimation("Floating");

        if (PlayerManager.instance.playerStatsHandler.currentChestPosessed != null) {
            PlayerManager.instance.playerStatsHandler.currentChestPosessed.SetActive(true);
        }

        while (Vector2.Distance(playerTransform.position, targetLocation) > 0.01f) {

            playerTransform.position = Vector2.MoveTowards(playerTransform.position, targetLocation, transferSpeed * Time.deltaTime);
            yield return null;

        }

        PlayerManager.instance.playerStatsHandler.SetChest(targetChest);
        targetChest.gameObject.SetActive(false);
        PlayerManager.instance.playerAnimationHandler.PlayAnimation("Possess");
        
        elapsedTime = 0;

        while(elapsedTime < chestPossessionAnimationLength) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        EndTransfer();
    }
	
}
