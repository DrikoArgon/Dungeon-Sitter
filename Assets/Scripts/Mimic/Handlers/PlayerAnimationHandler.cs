using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Animation Handler", menuName = "Player Handlers/ Player Animation Handler")]
public class PlayerAnimationHandler: ScriptableObject {

    private Animator animator;
    private Dictionary<string, float> animationLengths;

    public float currentAnimationLength;

    public RuntimeAnimatorController normalChestAnimator;

    private Coroutine animationTimingCoroutine;
    private Dictionary<ChestType, RuntimeAnimatorController> playerAnimators;

    public void Initialize() {

        animator = PlayerManager.instance.gameObject.GetComponent<Animator>();
        animationLengths = new Dictionary<string, float>();
        currentAnimationLength = 0;
        SetAnimationLengths();

        playerAnimators = new Dictionary<ChestType, RuntimeAnimatorController>();

        playerAnimators.Add(ChestType.Normal, normalChestAnimator);

        PlayerManager.instance.playerStatsHandler.OnChestTypeChange += UpdateAnimator;
    }

    public void UpdateAnimator() {
        animator.runtimeAnimatorController = new AnimatorOverrideController(playerAnimators[PlayerManager.instance.playerStatsHandler.currentChestType]);
        animationLengths.Clear();
        SetAnimationLengths();
    }

    public void PlayIdleAnimation() {
        animator.Play("Idle");
        currentAnimationLength = animationLengths["Idle"];

        if(animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }

        animationTimingCoroutine = PlayerManager.instance.StartCoroutine(CheckIdleDuration());
    }

    void PlaySuperIdle() {
        animator.Play("SuperIdle");
        currentAnimationLength = animationLengths["SuperIdle"];

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }

        animationTimingCoroutine = PlayerManager.instance.StartCoroutine(CheckSuperIdleDuration());
    }

    public void PlayWalkingAnimation(float directionHorizontal, float directionVertical) {

        animator.SetFloat("FaceX", directionHorizontal);
        animator.SetFloat("FaceY", directionVertical);

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }

        animator.Play("Walking");
    }

    public void PlayAttackAnimation() {

        animator.Play("Attack");

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }
    }

    public void PlayAnimation(string animationName) {
        if (animationLengths.ContainsKey(animationName)) {
            animator.Play(animationName);
        } else {
            Debug.LogError("Animation doesn't exist");
        }

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }
    }

    public void PlayChargingAnimation() {

        animator.Play("Charging");

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }
    }

    public void PlayFullChargeAnimation() {

        animator.Play("FullCharge");

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }
    }

    public void PlayChargedAnimation() {

        animator.Play("Charged");

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }
    }

    public void PlayChargedAttackAnimation() {

        animator.Play("ChargedAttack");

        if (animationTimingCoroutine != null) {
            PlayerManager.instance.StopCoroutine(animationTimingCoroutine);
            animationTimingCoroutine = null;
        }
    }

    void SetAnimationLengths() {

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips) {
            string[] words = clip.name.Split('_');
            string moveName = words[words.Length - 2];

            if (!animationLengths.ContainsKey(moveName)) {
                animationLengths.Add(moveName, clip.length);
            }
           
        }

    }

    public float GetAttackAnimationLength() {
        return animationLengths["Attack"];
    }

    public float GetAnimationLength(string animationName) {
        if (animationLengths.ContainsKey(animationName)) {
            return animationLengths[animationName];
        } else {
            Debug.LogError("Animation doesn't exist on lengths list");
            return 0.5f;
        }

    }

    IEnumerator CheckIdleDuration() {
        float elapsedTime = 0;
        float timeForSuperIdle = currentAnimationLength * 3;

        while (elapsedTime < timeForSuperIdle) {

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlaySuperIdle();
    }

    IEnumerator CheckSuperIdleDuration() {
        float elapsedTime = 0;

        while (elapsedTime < currentAnimationLength) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlayIdleAnimation();
    }


}
