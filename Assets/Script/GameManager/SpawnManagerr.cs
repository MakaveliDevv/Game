using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState { SPAWNING, WAITING, COUNTING }
public class SpawnManagerr : MonoBehaviour
{
    #region Singleton
    public static SpawnManagerr instance;
    void Awake() 
    {
        instance = this;
        enemyInstance = Enemy.instance;
    }
    #endregion

    public SpawnState state;
    private Enemy enemyInstance;

    public List<GameObject> enemies = new();
    public List<Transform> spawnLocations = new();

    public int enemyAmount;
    public int enemyCounter; // To keep track of the enemy
    
    public float rate;
    private int waveCount;

    public float timeBetweenWaves;
    [SerializeField] private float waveCountDown;

    private float searchCountDown = 1f;

    void Start() 
    {
        state = SpawnState.COUNTING;
        if(spawnLocations.Count == 0) 
        {
            Debug.LogError("There are no spawn locations assigned, please assign spawn locations in the inspector");
        }
        waveCount = 1;
        waveCountDown = timeBetweenWaves;
        enemyCounter = enemyAmount;
    } 

    void Update() 
    {
        if(state == SpawnState.WAITING) 
        {
            // Check if there are enemies alive
            if(!EnemyIsAlive()) 
            {
                // Wave completed, start next wave
                WaveCompleted();
            } 
            else { return; }  
        }

        if(waveCountDown <= 0) 
        {
            if(state != SpawnState.SPAWNING) 
            {
                // Spawn Wave
                StartCoroutine( SpawnEnemy() );
            }
        } else 
        {
            waveCountDown -= Time.deltaTime;
        }
    }


    void WaveCompleted() 
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        waveCount++;
        ResetEnemyCounter();
    }

    void ResetEnemyCounter() 
    {
        enemyCounter = enemyAmount;        
    }

    bool EnemyIsAlive() 
    {
        searchCountDown -= Time.deltaTime;

        if(searchCountDown <= 0f) 
        {
            searchCountDown = 1f;
            
            // Check if there are enemies
            if(enemyInstance == null && enemyCounter == 0) 
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnEnemy() 
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < enemyCounter; i++)
        {
            // Use modulo to cycle through the enemies list
            int enemyIndex = i % enemies.Count;

            // Spawn enemies
            InstantiateEnemy(enemies[enemyIndex]);
            yield return new WaitForSeconds(1f / rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }


    void InstantiateEnemy(GameObject _enemy) 
    {
        // GameObject enemy = waveInstance.enemies[Random.Range(0, waveInstance.enemies.Count)];
        Transform spawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)];
        Instantiate(_enemy, spawnPos.position, Quaternion.identity);
    }
}
