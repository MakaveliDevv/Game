using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/* This handles the behaviour of the Enemy */
public enum EnemyState { CHASE, ATTACK }

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyState state;
    public float inRangeRadius = 10f;
    private Transform target; // Player
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats stats;

    private float distance;

    void Start() 
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();

        agent.speed = stats.walkSpeed.ReturnBaseValue();
        // agent.stoppingDistance = 
    }

    void Update()
    {
        EnemyMovement();
    }    

    void FaceTarget() // Method to rotate the enemy towards the player 
    {
        // Get the direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Get the rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        
        // Then update the rotation to point in that direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, inRangeRadius);
    }

    void EnemyMovement() 
    {
        // Calculate the distance between the enemy and the target (player)
        distance = Vector3.Distance(target.position, transform.position);
        AttackState();
    }

    void ChaseState() 
    {
        // Set the enemy in a Chase State
        state = EnemyState.CHASE;

        // Then move the enemy towards the target destination
        agent.SetDestination(target.position); 

        // Play animation
    }

    void AttackState() 
    {
    	// Check when the enemy is to close to the target
        if(distance <= agent.stoppingDistance) 
        {
            // If so, set the enemy in an Attack State
            state = EnemyState.ATTACK;
            Debug.Log("Attacking");

            // Then get the CharacterStats component, this is needed for the Attack method
            CharacterStats targetStats = target.GetComponent<CharacterStats>();

            // Check if the component exsist
            if(targetStats != null) 
            {
                // Attack the target
                combat.Attack(targetStats);

                // Play attack animation

            }
            
            FaceTarget(); // Face the target

        } 
        else if(distance > agent.stoppingDistance) // Check if the target is not close anymore
        {
            // Then chase again
            ChaseState();
        }
    }
}
