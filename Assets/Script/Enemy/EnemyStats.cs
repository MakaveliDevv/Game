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
            GameManager.instance.enemyCounter--;
            GameManager.instance.deadCounter++;
               

            if(GameManager.instance.ShoudlSpawnGameObject(GameManager.instance.weapons))
            {
         
                GameManager.instance.InstantiateGameObject(GameManager.instance.weapons, transform);   
       
            } else if(GameManager.instance.ShoudlSpawnGameObject(GameManager.instance.powerups))
            {
 
                GameManager.instance.InstantiateGameObject(GameManager.instance.powerups, transform);   
            }  
        }
    }
}