using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    private GameObject[] enemy;

    void Update()
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject _enemy = enemy[Random.Range(0, enemy.Length)];

        Instantiate(_enemy, _sp.position, Quaternion.identity);
    }
}
