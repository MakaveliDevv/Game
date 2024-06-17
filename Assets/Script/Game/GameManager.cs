using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { MAIN_MENU, GAMEPLAY, PAUSE, GAMEOVER }
    public enum SpawnState { SPAWNING, WAITING, COUNTING }
    
    public SpawnState spawnState;
    public GameState gameState;

    [Header("GameObjects & Components")]
    private Enemy enemyInstance;
    [SerializeField] private GameObject inGameMenu; 
    

    #region Lists
    [Header("List")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    [SerializeField] private List<Transform> spawnLocations = new();
    public List<GameObject> weapons = new();
    public List<GameObject> powerups = new();
    #endregion

    #region EnemyStuff
    [Header("Enemy Stuff")]
    public List<GameObject> enmiesInGame = new();
    [SerializeField] private int initialEnemyAmount; // Initial amount for the first wave
    private int enemyAmount;
    public int enemyCounter; // To keep track of the enemy
    public int deadCounter;
    public float spawnRate;
    private float searchCountDown = 1f;
    #endregion

    #region WaveStuff
    [Header("Wave Stuff")]
    [SerializeField] private int waveCounter;
    [SerializeField] private float timeBetweenWaves;
    private float waveCountDown;
    [SerializeField] private TextMeshProUGUI waveCounterText;
    [SerializeField] private TextMeshProUGUI enemyCounterText;
    #endregion

    #region MainMenuStuff
    [Header("MainMenu Stuff")]
    [SerializeField] private GameObject difficultyButton;
    [SerializeField] private GameObject difficultyPanel;
    [SerializeField] private string selectedDifficulty;

    // [SerializeField] private TextMeshProUGUI text;
    #endregion 

    [Header("GameManagementStuff")]
    public TextMeshProUGUI timerText; 
    public bool isTimerRunning; 
    public bool tabPressed;
    public float elapsedGameplayTime;

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
        // DontDestroyOnLoad(gameObject); // Ensure that this object persists between scenes

    }

    #endregion
    void Start()
    {
        // if(text != null) text = difficultyButton.GetComponentInChildren<TextMeshProUGUI>();

        if(IsSceneActive("GamePlayScene"))
        {
            elapsedGameplayTime = 0f;
            isTimerRunning = true;
        } 

        enemyInstance = Enemy.instance;
        spawnState = SpawnState.COUNTING;

        if (spawnLocations.Count == 0)
        {
            Debug.LogError("There are no spawn locations assigned, please assign spawn locations in the inspector");
        }

        waveCounter = 1;
        waveCountDown = timeBetweenWaves;

        enemyAmount = initialEnemyAmount;
        enemyCounter = enemyAmount;
    }

    void Update()
    {
        UpdateTimer();
        HandleInput();

        if(IsSceneActive("MainMenu"))
            gameState = GameState.MAIN_MENU;

        else if(IsSceneActive("GamePlayScene") || IsSceneActive("SampleScene"))
        {
            gameState = GameState.GAMEPLAY;


            waveCounterText.text = waveCounter.ToString();
            enemyCounterText.text = enemyCounter.ToString();
            
            if (spawnState == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                    WaveCompleted();
            
                else 
                    return;
            }

            if (waveCountDown <= 0)
            {
                if (spawnState != SpawnState.SPAWNING)
                {
                    // Spawn Wave
                    StartCoroutine(SpawnEnemy());
                    waveCountDown = 0;
                }
            }
            else
                waveCountDown -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.M)) 
        {
            tabPressed = true;
            // isInGameMenu = true;
        }

        if(Input.GetKeyUp(KeyCode.M)) 
        {
            tabPressed = false;
            // isInGameMenu = false;
        }

        // switch (gameState)
        // {
        //     case(GameState.MAIN_MENU):
        //         Debug.Log("We are in the Main menu scene");

        //     break;

        //     case(GameState.GAMEPLAY):
        //         // StartTimer();
        //         // Resume the game timer
        //         // Activate agent

        //     break;

        //     case(GameState.PAUSE):
        //         // Stop the game timer
        //         // Stop the agent

        //     break;

        //     case(GameState.GAMEOVER):
        //         // Stop the game timer
        //         // Stop the agent
        //         // Switch to Main menu scene

        //     break;
        // }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabPressed = true; // Toggle the menu state
            SetMenuOpen(tabPressed);
        }

        if(Input.GetKeyUp(KeyCode.Tab))
        {
           tabPressed = false;
           SetMenuOpen(tabPressed);
        }
    }

    void UpdateTimer()
    {
        if (isTimerRunning)
        {
            elapsedGameplayTime += Time.deltaTime;
            UpdateTimerText(elapsedGameplayTime);
        }
    }

    
    void SetMenuOpen(bool isOpen)
    {
        if (isOpen)
        {
            StopTimer();
        }
        else
        {
            StartTimer();
        }
    }

    void StopTimer()
    {
        isTimerRunning = false;
    }

    void StartTimer()
    {
        isTimerRunning = true;
    }

    void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    // Method to handle in-game menu open/close
    public void SetMenuOpen()
    {
        if (tabPressed)  
        {
            StopTimer();
        }
        
        if(!tabPressed)
        {
            StartTimer();
        }
    }

    private bool IsSceneActive(string scenename) 
    {
        return SceneManager.GetActiveScene().name == scenename;
    }

    void WaveCompleted()
    {
        spawnState = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        waveCounter++;
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
        spawnState = SpawnState.SPAWNING;

        for (int i = 0; i <= enemyCounter; i++)
        {
            // Spawn enemies
            InstantiateEnemy();
            yield return new WaitForSeconds(1f / spawnRate);
        }

        spawnState = SpawnState.WAITING;
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

    public void SwitchScene(string sceneName) 
    {
        // PlayerPrefs.SetString("Difficulty", selectedDifficulty);
        SceneManager.LoadScene(sceneName);
    }

    // public void SwitchToDifficultyPanel() 
    // {
    //     if(difficultyButton != null) 
    //     {
    //         difficultyButton.GetComponent<Button>().enabled = false;
    //         difficultyButton.GetComponent<Image>().enabled = false;
    //         text.gameObject.SetActive(false);
    //         // difficultyButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);


    //         difficultyPanel.SetActive(true);
    //     }
    // }

    // public void SelectDifficulty(string difficulty) 
    // {
    //     // Store the selected difficulty
    //     selectedDifficulty = difficulty;

    //     difficultyPanel.SetActive(false);

    //     difficultyButton.SetActive(true);
    //     difficultyButton.GetComponent<Button>().enabled = true;
    //     difficultyButton.GetComponent<Image>().enabled = true;
    //     text.gameObject.SetActive(true);
    //     // difficultyButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(true);
    // }

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
