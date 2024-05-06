using UnityEngine;

/* Handles the interaction with the Enemy */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    public static Enemy instance;
    private Transform target; // player
    private CharacterStats enemyStats;

    void Awake() 
    {
        instance = this;
        target = PlayerManager.instance.player.transform;
    }
    

    void Start() 
    {
        enemyStats = GetComponent<CharacterStats>();
    }

    // public override void Interact()
    // {
    //     base.Interact();

    //     if(target.TryGetComponent<CharacterCombat>(out var playerCombat)) 
    //     {
    //         playerCombat.Attack(enemyStats); // Getting attacked
    //     }
    // }
}
