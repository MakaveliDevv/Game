using System.Collections;
using System.Collections.Generic;
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
            Destroy(this.gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); // Ensure that this object persists between scenes
    }
    #endregion 
    public SpawnState state;

    [Header("GameObjects & Components")]
    private Enemy enemyInstance;

    [Header("List")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    [SerializeField] private List<Transform> spawnLocations = new();
    [SerializeField] private List<GameObject> weapons = new();
    [SerializeField] private List<GameObject> powerups = new();

    [Header("Enemy Stuff")]
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

    public void InstantiateWeapon(Transform _transform)
    {
        GameObject wpn = weapons[Random.Range(0, weapons.Count)];
        Instantiate(wpn, _transform.position, Quaternion.identity);
        Debug.Log(wpn.name);
    }

    public void InstantiatePowerup(Transform _transform) 
    {
        GameObject buff = powerups[Random.Range(0, powerups.Count)];
        Instantiate(buff, _transform.position, Quaternion.identity);
        Debug.Log(buff.name);
    }

    public bool ShouldDropWeapon()
    {
        float randomValue = Random.Range(0f, 1f);

        foreach (var weapon in weapons)
        {
            WeaponPickup weaponPickup = weapon.GetComponent<WeaponPickup>();
            float dropRate = weaponPickup.weapon.dropRate;

            if(randomValue <= dropRate) 
            {
                return true;
            }
        }

        return false;
    }

    public bool ShouldDropPowerup() 
    {
        float randomValue = Random.Range(0f, 1f);

        foreach (var powerup in powerups)
        {
            ItemPickup itemPickup = powerup.GetComponent<ItemPickup>();
            float dropRate = itemPickup.item.dropRate;

            if(randomValue <= dropRate) 
            {
                return true;
            }
        }

        return false;    
    }
    
    public bool ShouldDropPowerupp() 
    {
        float dropRate = .5f;
        float rand = Random.Range(0f, 1f);

        if(rand <= dropRate) 
            return true;
        else
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
