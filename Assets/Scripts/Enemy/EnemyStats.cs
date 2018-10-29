using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/New Enemy Stats")]
public class EnemyStats : ScriptableObject {

    public int maxHealth;
    public float walkingSpeed;
    public float chasingSpeed;

    public ChestType preferredChestType;

    public float innocenceLevel;

    public int initialTreasureAmount;

}
