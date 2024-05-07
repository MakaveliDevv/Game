using UnityEngine;

public class EnemyStats : CharacterStats
{
    public bool deathIsComingForYou;

    public override void Die()
    {
        base.Die();

        deathIsComingForYou = true;

        if(deathIsComingForYou == true) 
        {
            Destroy(gameObject);
            gameManager_instance.enemyCounter--;
            gameManager_instance.deadCounter++;
               

            if(GameManager.instance.ShoudlSpawnGameObject(GameManager.instance.weapons))
            {
                // foreach (var weapon in GameManager.instance.weapons)
                // {
                    GameManager.instance.InstantiateGameObject(GameManager.instance.weapons, transform);   
                // }

            } else if(GameManager.instance.ShoudlSpawnGameObject(GameManager.instance.powerups))
            {
                // foreach (var powerup in GameManager.instance.powerups)
                // {
                    GameManager.instance.InstantiateGameObject(GameManager.instance.powerups, transform);   
                // }
            }  
                
                
            // else if(GameManager.instance.ShouldDropPowerup()) 
            //     GameManager.instance.InstantiatePowerup(transform);

            // if(GameManager.instance.ShouldDropWeapon()) 
            //     GameManager.instance.InstantiateWeapon(transform);
                
            // else if(GameManager.instance.ShouldDropPowerup()) 
            //     GameManager.instance.InstantiatePowerup(transform);
        }
    }
        // Calculate a x amount of percentage that the weapon can drop

        // Base
        // float weaponSpawnPercentage = 0.1f; // Example: 10% of dead enemies spawn a weapon
        // float deadPercentage = GameManager.instance.deadCounter / (float)GameManager.instance.initialEnemyAmount;

        // Check if the percentage of dead enemies is greater than or equal to the desired percentage
        // return deadPercentage >= weaponSpawnPercentage;
}