using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private SpawnManagerr spawnManagerInstance;    
    public bool deathIsComingForYou;
    void Awake() 
    {
        spawnManagerInstance = SpawnManagerr.instance;
    }

    public override void Die()
    {
        base.Die();

        deathIsComingForYou = true;
        spawnManagerInstance.InstantiateWeapon(this.transform);

        if(deathIsComingForYou == true) 
        {
            // Add death animation
            Destroy(gameObject);

            // Decrement the enemy counter
            spawnManagerInstance.enemyCounter--;
            spawnManagerInstance.deathCounter++;
        }
    }
}