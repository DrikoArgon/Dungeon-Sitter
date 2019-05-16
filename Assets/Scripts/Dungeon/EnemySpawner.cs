using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner instance;

    public List<Room> dungeonEntrances;

    public DungeonWavesData wavesData;

    [SerializeField]
    private DungeonWave currentWave;
    [SerializeField]
    private int currentWaveIndex;
    [SerializeField]
    private int currentEnemyIndex;

    private float currentTime;
    private float currentTimeBetweenEnemies;
    private float currentWaveTimeBetweenEnemies;

    [SerializeField]
    private bool isWaitingNextWave;

    public bool dungeonWavesEnded;

    private void Awake() {
        if(instance != null) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start() {
        StartWaves();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update() {
        if (wavesData != null) {
            if (!dungeonWavesEnded) {
                HandleSpawns();
            }
        }
    }

    void HandleSpawns() {

        currentTime += Time.deltaTime;

        if (!isWaitingNextWave) {
            currentTimeBetweenEnemies += Time.deltaTime;

            if (currentTimeBetweenEnemies >= currentWaveTimeBetweenEnemies) {

                currentTimeBetweenEnemies = 0;
                SpawnEnemy();

                currentEnemyIndex++;

                if (currentEnemyIndex >= currentWave.waveEnemies.Count) {
                    isWaitingNextWave = true;
                } 

            }
        }

        if(wavesData.dungeonWaves.Count > 1 && isWaitingNextWave) {
            if (currentTime >= wavesData.timeBetweenWaves) {
                SetNextWave();
            }
        }

    }

    public void StartWaves() {
        currentWave = wavesData.dungeonWaves[0];
        currentWaveTimeBetweenEnemies = currentWave.timeBetweenEachEnemy;
    }

    public void SetNextWave() {
        Debug.Log("Initiating next wave");
        currentEnemyIndex = 0;
        currentWaveIndex++;

        if (currentWaveIndex >= wavesData.dungeonWaves.Count) {
            //Arena has no more Waves
            dungeonWavesEnded = true;
        } else {
            currentWave = wavesData.dungeonWaves[currentWaveIndex];
            currentTimeBetweenEnemies = 0;
            currentWaveTimeBetweenEnemies = currentWave.timeBetweenEachEnemy;
            isWaitingNextWave = false;
        }
    }

    public void SpawnEnemy() {

        int randomIndex = Random.Range(0, dungeonEntrances.Count);

        Room entrance = dungeonEntrances[randomIndex];

        GameObject newEnemy = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/" + currentWave.waveEnemies[currentEnemyIndex].ToString()), entrance.entranceEnemySpawnPoint.position, Quaternion.identity);

        newEnemy.GetComponentInChildren<EnemyMovementHandler>().DefineTarget(entrance.GetArrivalPoint());
        newEnemy.GetComponentInChildren<EnemyObservationHandler>().targetRoom = entrance;

        UIManager.instance.ShowEnemyEnterNotification();

    }



}
