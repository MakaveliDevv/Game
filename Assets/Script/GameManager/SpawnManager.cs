using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave 
    {
        public string name;
        public GameObject[] enemy;
        public int count;
        public float rate;
   }

    public Wave[] waves;
    public SpawnState state;
    public Transform[] spawnPoints;
    private int nextWave;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private float searchCountDown = 1f;

    void Start() 
    {
        state = SpawnState.COUNTING;
        if(spawnPoints.Length == 0) 
        {
            Debug.LogError("No spawn points referenced.");
        }
        waveCountdown = timeBetweenWaves;
    }

    void FixedUpdate() 
    {
        if(state == SpawnState.WAITING) 
        {
            // Check if enemies are still alive
            if(!EnemyIsAlive()) 
            {
                WaveCompleted();
            }
            else { return; }
        }

        if (waveCountdown <= 0) 
        {
            if (state != SpawnState.SPAWNING) 
            {
                StartCoroutine( SpawnWave( waves[ nextWave ] ));   
            }
        }
        else 
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted() 
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1) 
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETED! Looping...");
        } 
        else 
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive() 
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f) 
        {
            searchCountDown = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemy") == null) 
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) 
    {
        state = SpawnState.SPAWNING;
        
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy[i]);
            yield return new WaitForSeconds( 1f/_wave.rate );
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy) 
    {
        Transform _sp = spawnPoints[ Random.Range(0, spawnPoints.Length) ];
        Instantiate(_enemy, _sp.position, Quaternion.identity);
    }
}
