using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] private CharacterCombat combat;
    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.CompareTag(Tags.PLAYER)) 
        {
            Debug.Log("PlayerHit");
            if(collider.TryGetComponent<PlayerStats>(out var playerStats))
            {
                combat.Attack(playerStats);
            }
        }
    }
}
