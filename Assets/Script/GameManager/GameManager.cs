using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Wave wave;
    private int nextWave;

    void Start()
    {
        wave = Wave.instance;
        // StartCoroutine(SpawnWaveRoutine());
    }

    // void Update()
    // {
    //     wave.UpdateWave();
    // }

    // IEnumerator SpawnWaveRoutine()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(wave.timeBetweenWaves);

    //         // Increment waveCount and reset countdown
    //         wave.waveCount++;
    //         wave.waveCountDown = wave.timeBetweenWaves;

    //         // Spawn enemies
    //         for (int i = 0; i < wave.waveCount; i++)
    //         {
    //             wave.SpawnEnemy();
    //             yield return new WaitForSeconds(1f / wave.spawnRate);
    //         }

    //         // Wait for all enemies to be defeated
    //         while (wave.EnemyCount() > 0)
    //         {
    //             yield return null;
    //         }
    //     }
    // }
}
