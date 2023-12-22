using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private GameManager instance;    
    public bool deathIsComingForYou;
    void Awake() 
    {
        instance = GameManager.instance;
    }

    public override void Die()
    {
        base.Die();

        deathIsComingForYou = true;
        instance.InstantiateWeapon(this.transform);

        if(deathIsComingForYou == true) 
        {
            // Add death animation
            Destroy(gameObject);

            // Decrement the enemy counter
            instance.enemyCounter--;
            instance.deadCounter++;
            if (ShouldSpawnWeapon())
            {
                GameManager.instance.InstantiateWeapon(this.transform); // Pass the transform as a parameter
            }
        }
    }

    bool ShouldSpawnWeapon()
    {
        float weaponSpawnPercentage = 0.1f; // Example: 10% of dead enemies spawn a weapon
        float deadPercentage = GameManager.instance.deadCounter / (float)GameManager.instance.initialEnemyAmount;

        // Check if the percentage of dead enemies is greater than or equal to the desired percentage
        return deadPercentage >= weaponSpawnPercentage;
    }
}