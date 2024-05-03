using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   void OnTriggerEnter(Collider collider) 
    {
        Destroy(gameObject);

        if(collider.CompareTag(Tags.ENEMY)) 
        {
            Debug.Log("Hitt an enemy: " + collider.name);
            
            // // Deal 
            // if(collider.TryGetComponent<CharacterCombat>(out var playerCombat)) 
            // {
            //     playerCombat.Attack(enemyStats); // Takes damage
            // }
        }
    }
}
