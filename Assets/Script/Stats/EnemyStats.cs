using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private SpawnManagerr spawnManagerInstance;
    
    void Awake() 
    {
        spawnManagerInstance = SpawnManagerr.instance;
    }

    public override void Die()
    {
        base.Die();

        // Add death animation
        Destroy(gameObject);

        // Decrement the enemy counter
        spawnManagerInstance.enemyCounter--;
    }
}
