using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour {

    private Animator enemyAnimator;
    private Dictionary<string, float> animationLengths;

    // Use this for initialization
    void Start() {
        enemyAnimator = GetComponent<Animator>();

        animationLengths = new Dictionary<string, float>();

    }

    public void PlayAnimation(string animationName) {
        enemyAnimator.Play(animationName);
    }

    public void PlayHurtAnimation() {
        enemyAnimator.Play("Hurt", -1, 0);
    }

    public void PlayWalkingAnimation(Vector2 direction) {

        enemyAnimator.SetFloat("FaceX", direction.x);
        enemyAnimator.SetFloat("FaceY", direction.y);

        enemyAnimator.Play("Walk");
    }

    public void PlayRunningAnimation(Vector2 direction) {

        enemyAnimator.SetFloat("FaceX", direction.x);
        enemyAnimator.SetFloat("FaceY", direction.y);

        enemyAnimator.Play("Run");
    }

    void SetAnimationLengths() {

        AnimationClip[] clips = enemyAnimator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips) {
            string[] words = clip.name.Split('_');
            string moveName = words[words.Length - 2];

            if (!animationLengths.ContainsKey(moveName)) {
                animationLengths.Add(moveName, clip.length);
            }

        }
    }

    public float GetAnimationLength(string animationName) {
        if (animationLengths.ContainsKey(animationName)) {
            return animationLengths[animationName];
        } else {
            Debug.LogError("Animation doesn't exist on lengths list");
            return 0.5f;
        }

    }
}
