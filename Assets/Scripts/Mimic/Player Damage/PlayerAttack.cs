using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : ScriptableObject {

    public int damage;
    public float attackCooldown;
    public float attackAnimationLength;

    protected float attackTimer;
    protected float animationPercent;

    public bool isOnCooldown;

    public abstract void StartAttack(float animationLength); //Initiates the attack, setting the duration of the animation

    public abstract void ProcessAttack(); //Process attack movement, instantiation of effects and other actions of the attack

    public abstract void InterruptAttack(); //Interrupts the attack suddenly if something happens like receive damage for example

    public abstract void FinishAttack(); //Finishes the attack and notifies the attack handler
}
