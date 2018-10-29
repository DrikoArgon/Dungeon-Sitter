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
