using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Dungeon Waves Data")]
public class DungeonWavesData : ScriptableObject {

    //[HideInInspector]
    public float timeBetweenWaves;

    //[HideInInspector]
    public List<DungeonWave> dungeonWaves = new List<DungeonWave>();

}

[System.Serializable]
public struct DungeonWave {
    public List<EnemyTypes> waveEnemies;
    public float timeBetweenEachEnemy;
}

//The names in this enum must match the enemy's prefab name.
public enum EnemyTypes {
    Warrior
}
