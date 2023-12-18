using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { PATROL, CHASE, ATTACK }

public class EnemyController : MonoBehaviour
{
    private EnemyState state;
    private NavMeshAgent navAgent;
    private GameObject target;
    public float walkSpeed, 
    runSpeed, 
    chase_distance, 
    attack_distance, 
    chaseAfterAttack_distance;
    private float currentChase_distance;
    public float minPatrol_distance, maxPatrol_distance;
    public float patrol_time;
    private float patrol_counter;
    public float attack_time;
    private float attack_counter;
    
    void Awake() 
    {
        target = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        state = EnemyState.PATROL; // set the enemy state on patrol
        patrol_time = patrol_counter;// start the patrol timer
        attack_time = attack_counter; // start the attack timer
        currentChase_distance = chase_distance;
    }

    void Update()
    {
        switch (state) 
        {
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.CHASE:
                Chase();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
            default:
                break;
        }
    }

    public void Patrol() 
    {
        // player can move again
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        // add time to the patrol timer
        patrol_counter += Time.deltaTime;
        if(patrol_counter > patrol_time) // if the patrol time is over
        {
            SetNewRandomDirection(); // set a new direction
            patrol_counter = 0f; // reset the patrol timer
        }

        if(navAgent.velocity.sqrMagnitude > 0f) 
        {
            // play walk animation
        }
        else 
        {
            // stop walk animation
        }

        // check if the distance between the player and the enemy is less than the chase distance
        if(Vector3.Distance(transform.position, target.transform.position) <= chase_distance) 
        {
            // enemy chase player
            state = EnemyState.CHASE;
        }
    } // patrol

    public void Chase() 
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;

        navAgent.SetDestination(target.transform.position); // set the destination towards the player
        
        if(navAgent.velocity.sqrMagnitude > 0f) 
        {
            // play run animation
        }
        else 
        {
            // stop run animation
        }

        if(Vector3.Distance(transform.position, target.transform.position) <= attack_distance) 
        {
            state = EnemyState.ATTACK;

            // reset the chase distance back to default
            if(chase_distance != currentChase_distance) 
            {
                chase_distance = currentChase_distance;
            }
        } 
        else if (Vector3.Distance(transform.position, target.transform.position) > chase_distance)
        {
            state = EnemyState.PATROL;

            // reset the chase distance back to default
            patrol_counter = patrol_time;
            if(chase_distance != currentChase_distance) 
            {
                chase_distance = currentChase_distance;
            }
        }
    } // chase

    public void Attack() 
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_time += Time.deltaTime; // start the attack timer
    
        if(attack_time > attack_counter)
        {
            attack_time = 0f; // reset the attack timer
            // start attacking

            // play animation

            if(Vector3.Distance(transform.position, target.transform.position) > attack_distance) 
            {
                // stop attacking and start chasing again
                state = EnemyState.CHASE;

                // stop attack animation
            }

        }
    } // attack

    public void SetNewRandomDirection() 
    {
        float radius = Random.Range(minPatrol_distance, maxPatrol_distance);
        Vector3 direction = Random.insideUnitSphere * radius;

        direction += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(direction, out navHit, radius, -1);

        navAgent.SetDestination(navHit.position);
    }
}
