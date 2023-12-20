using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the interaction with the Enemy */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private CharacterStats stats;

    public override void Start() 
    {
        base.Start();
        stats = GetComponent<CharacterStats>();
    }

    // public override void Interact()
    // {
    //     base.Interact();

    //     CharacterCombat playerCombat = target.GetComponent<CharacterCombat>();
    //     if(playerCombat != null) 
    //     {
    //         playerCombat.Attack(stats);
    //     }
    // }
}
