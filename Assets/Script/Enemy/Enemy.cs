using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the interaction with the Enemy */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    public static Enemy instance;

    void Awake() 
    {
        instance = this;
    }
    
    private CharacterStats enemyStats;

    public override void Start() 
    {
        base.Start();
        enemyStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        // Do damage to the player
        if(target.TryGetComponent<CharacterCombat>(out var playerCombat)) 
        {
            playerCombat.Attack(enemyStats); // Takes damage
        }
    }
}
