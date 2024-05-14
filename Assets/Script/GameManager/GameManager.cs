using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }
    
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of the WeaponInventory found!");
            Destroy(gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Ensure that this object persists between scenes
    }
    #endregion 
    public SpawnState state;

    [Header("GameObjects & Components")]
    private Enemy enemyInstance;

    [Header("List")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    [SerializeField] private List<Transform> spawnLocations = new();
    public List<GameObject> weapons = new();
    public List<GameObject> powerups = new();

    [Header("Enemy Stuff")]
    public List<GameObject> enmiesInGame = new();
    [SerializeField] private int initialEnemyAmount; // Initial amount for the first wave
    private int enemyAmount;
    public int enemyCounter; // To keep track of the enemy
    public int deadCounter;
    public float spawnRate;
    private float searchCountDown = 1f;

    [Header("Wave Stuff")]
    [SerializeField] private int waveCount;
    [SerializeField] private float timeBetweenWaves;
    private float waveCountDown;


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
            if (!EnemyIsAlive())
                WaveCompleted();
        
            else 
                return;
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Spawn Wave
                StartCoroutine(SpawnEnemy());
                waveCountDown = 0;
            }
        }
        else
            waveCountDown -= Time.deltaTime;
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        waveCount++;
        IncreaseEnemyAmountPerWave(); // Increase the number of enemies for the next wave
        ResetEnemyCounter();
    }

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
            if (enemyInstance == null && enemyCounter == 0)
            {
                return false;
            }
        }

        return true;
    }

    void InstantiateEnemy()
    {
        GameObject enemy = enemyTypes[Random.Range(0, enemyTypes.Count)];
        Transform spawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)];

        Instantiate(enemy, spawnPos.position, spawnPos.transform.rotation);
        enmiesInGame.Add(enemy);
    }

    IEnumerator SpawnEnemy()
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i <= enemyCounter; i++)
        {
            // Spawn enemies
            InstantiateEnemy();
            yield return new WaitForSeconds(1f / spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    public void InstantiateGameObject(List<GameObject> _gameObjects, Transform _transform) 
    {
        Instantiate(_gameObjects[Random.Range(0, _gameObjects.Count)], _transform.position, Quaternion.identity);
    }   

    public bool ShoudlSpawnGameObject(List<GameObject> _gameObjects) 
    {
        float randomValue = Random.Range(0f, 1f);
        
        foreach (var gameObject in _gameObjects)
        {
            if(gameObject.TryGetComponent<IDGameObject>(out var pickupComponent)) 
            {
                float dropRate = pickupComponent.GetDropRate();
                if(randomValue <= dropRate) 
                {
                    return true;
                }
            }
        }

        return false;
    }

    // bool ShouldSpawnWeapon()
    // {
    //     // Calculate a percentage of the drop rate for each enemy killed

    //     // For every x amount of enemies killed, spawn a weapon
    //     // Calculate the x amount of enemies based on a percentage
    //     // The percentage should be 
    //     float weaponSpawnPercentage = 0.1f; // Example: 10% of dead enemies spawn a weapon
    //     float deadPercentage = deadCounter / (float)initialEnemyAmount;

    //     // Check if the percentage of dead enemies is greater than or equal to the desired percentage
    //     return deadPercentage >= weaponSpawnPercentage;
    // }
}
