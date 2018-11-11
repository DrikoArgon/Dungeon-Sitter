using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public ChestType chestType;
    public Transform arrivalPoint;
    public int treasureAmount;
    public PlayerDirection chestDirection;

    private Enemy currentEnemyInteracting;
    private Animator chestAnimator;
    private Dictionary<string, float> animationLengths;

    public bool isTargeted;
    public bool isDisabled;

    private void Awake() {
        chestAnimator = GetComponent<Animator>();
        animationLengths = new Dictionary<string, float>();
    }

    // Use this for initialization
    void Start () {
        SetAnimationLengths();
    }

    public virtual void OpenChest(Enemy _currentEnemyInteracting) {

        currentEnemyInteracting = _currentEnemyInteracting;
        StartCoroutine(ProcessOpenChest());
        isDisabled = true;
    }

    void SetAnimationLengths() {

        AnimationClip[] clips = chestAnimator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips) {
            string[] words = clip.name.Split('_');
            string moveName = words[words.Length - 2];

            if (!animationLengths.ContainsKey(moveName)) {
                animationLengths.Add(moveName, clip.length);
            }

        }
    }

    protected void PlayOpenAnimation() {
        chestAnimator.Play("Open");
    }

    protected IEnumerator ProcessOpenChest() {

        float elapsedTime = 0;
        float openChestAnimationLength = animationLengths["Open"];
        PlayOpenAnimation();

        while (elapsedTime < openChestAnimationLength) {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        DungeonManager.instance.dungeonTreasureManager.DecreaseAmountOfTreasure(treasureAmount);
        currentEnemyInteracting.GetComponent<EnemyObservationHandler>().isWaitingForChestToOpen = false;
    }
}
