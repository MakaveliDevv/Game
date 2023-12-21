using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the interaction with the Enemy */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    #region Singleton
    public static Enemy instance;

    void Awake() 
    {
        instance = this;
    }
    #endregion
    private CharacterStats enemyStats;

    public override void Start() 
    {
        base.Start();
        enemyStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        // base.Interact();

        CharacterCombat playerCombat = target.GetComponent<CharacterCombat>();
        if(playerCombat != null) 
        {
            playerCombat.Attack(enemyStats);
        }
    }
}
