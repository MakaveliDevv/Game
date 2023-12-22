using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState { SPAWNING, WAITING, COUNTING }

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of the WeaponInventory found!");
            Destroy(this.gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); // Ensure that this object persists between scenes
    }
    #endregion 

    public SpawnState state;
    private Enemy enemyInstance;

    public List<GameObject> enemies = new();
    public List<Transform> spawnLocations = new();
    public List<GameObject> weapons = new();

    public int initialEnemyAmount; // Initial amount for the first wave
    private int enemyAmount;

    public int enemyCounter; // To keep track of the enemy
    public int deadCounter;

    public float spawnRate;
    private int waveCount;

    public float timeBetweenWaves;
    [SerializeField] private float waveCountDown;

    private float searchCountDown = 1f;

    void Start()
    {
        enemyInstance = Enemy.instance;
        state = SpawnState.COUNTING;

        if (spawnLocations.Count == 0)
        {
            Debug.LogError("There are no spawn locations assigned, please assign spawn locations in the inspector");
        }

        waveCount = 1;
        waveCountDown = timeBetweenWaves;

        enemyAmount = initialEnemyAmount;
        enemyCounter = enemyAmount;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if there are enemies alive
            if (!EnemyIsAlive())
            {
                // Wave completed, start next wave
                WaveCompleted();
            }
            else { return; }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Spawn Wave
                StartCoroutine(SpawnEnemy());
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        waveCount++;
        // CalculatePercentageOfAliveEnemies(); // Calculate the percentage of alive enemies for the next wave
        IncreaseEnemyAmountPerWave(); // Increase the number of enemies for the next wave
        ResetEnemyCounter();
    }

    // void CalculatePercentageOfAliveEnemies()
    // {
    //     float percentage = 0.35f; // Example: 35% of alive enemies for the next wave

    //     // Calculate the percentage of alive enemies for the next wave
    //     int aliveEnemies = Mathf.RoundToInt(initialEnemyAmount * percentage);
    //     enemyCounter = Mathf.Min(aliveEnemies, enemyCounter);
    // }

    void IncreaseEnemyAmountPerWave()
    {
        float increasePercentage = 0.35f; // 35%

        // Calculate the increase based on the percentage
        int increaseAmount = Mathf.RoundToInt(enemyAmount * increasePercentage);

        // Increment the enemyAmount for the next wave
        enemyAmount += increaseAmount;
    }

    void ResetEnemyCounter()
    {
        enemyCounter = enemyAmount;
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;

            // Check if there are enemies
            if (enemyInstance == null && enemyCounter == 0)
            {
                return false;
            }
        }
        return true;
    }

    void InstantiateEnemy()
    {
        GameObject enemy = enemies[Random.Range(0, enemies.Count)];
        Transform spawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)];

        Instantiate(enemy, spawnPos.position, spawnPos.transform.rotation);
    }

    IEnumerator SpawnEnemy()
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < enemyCounter; i++)
        {
            // Spawn enemies
            InstantiateEnemy();
            yield return new WaitForSeconds(1f / spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }


    public void InstantiateWeapon(Transform _transform)
    {
        GameObject wpn = weapons[Random.Range(0, weapons.Count)];
        Instantiate(wpn, _transform.position, _transform.rotation);
    }
}
