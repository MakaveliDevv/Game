using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Wave : GameManager
{
    #region Singleton
    public static Wave instance;

    void Awake() 
    {
        instance = this;
    }

    #endregion
    public enum WaveState { SPAWNING, WAITING, NOTHING };
    
    public List<GameObject> enemies = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();

    // public int waveCount;
    // public float spawnRate;

    public float timeBetweenWaves;
    public float waveCountDown;

    private int enemyCount; // Keeps track of enemies amount
    public int enemyAmount;

    public WaveState state;

    void Start()
    {
        state = WaveState.WAITING;
        waveCountDown = timeBetweenWaves;

        enemyCount = enemyAmount;
    }

    void Update() 
    {
        if(state == WaveState.WAITING) 
        {
            waveCountDown -= Time.deltaTime;

            if(waveCountDown <= 0) 
            {
                // Set the state on spawning
                state = WaveState.SPAWNING;

                // Generate random spawn position
                Transform randomSpawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)];

                // Generate random enemies
                GameObject randomEnemy = enemies[Random.Range(0, enemies.Count)];
                
                // Spawn enemies
                for (int i = 0; i < enemyAmount; i++)
                {
                    Instantiate(randomEnemy, randomSpawnPos.position, Quaternion.identity);
                }
            
                state = WaveState.NOTHING;
            }
        }

        // Check if the enemie count reached to 0
        if(enemyCount == 0) 
        {
            // Set the state on Waiting
            state = WaveState.WAITING;
        }
    }

    void SpawnEnemy(GameObject _enemy) 
    {
        
    }

    // void SpawnEnemy() 
    // {
    //     // Check if the Wave State is set to Waiting
    //     if(state == WaveState.WAITING ) 
    //     {
    //         // And the waveCountDown reached zero
    //         if(waveCountDown <= 0) 
    //         {
                
    //             // Set the Wave State on Spawning
    //             state = WaveState.SPAWNING;

    //             // Get a random spawn location
    //             Transform spawnPoint = spawnLocations[Random.Range(0, spawnLocations.Count)];

    //             // Instantiate enemies
    //             Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPoint.position, Quaternion.identity);

    //             // Set then Wave State to Nothing
    //             state = WaveState.NOTHING;
    //         } 
    //     }
    // }
}
