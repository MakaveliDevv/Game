using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/* This handles the behaviour of the Enemy */

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))] 
public class EnemyController : MonoBehaviour
{
    public enum EnemyState { CHASE, ATTACK, KNOCKEDBACK, IDLE}
    [SerializeField] private EnemyState state;
    // [SerializeField] private EnemyState previousState;

    public float inRangeRadius = 10f;
    private Transform target; // Player
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats stats;
    private Rigidbody rb;
    private CapsuleCollider capsColl;
    [Range(0.00f, .1f)] [SerializeField] private float stillTreshold = .05f;

    void Awake() 
    {
        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        
        rb = GetComponent<Rigidbody>();
        capsColl = GetComponent<CapsuleCollider>();

    }

    void Start() 
    {
        state = EnemyState.IDLE;
        agent.speed = stats.walkSpeed.ReturnBaseValue();
    }

    void FixedUpdate() 
    {
        Movement();
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.CHASE:
                ChaseState();
                               
            break;

            case EnemyState.ATTACK:
                AttackState();

            break;

            case EnemyState.KNOCKEDBACK:
                Debug.Log("Knockback state");

            break;
            // default: 
        }
    }    

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, inRangeRadius);
    }

    private void Movement() 
    {
        transform.position = new(transform.position.x, 0f, transform.position.z);
        agent.transform.position = new(transform.position.x, transform.position.y, transform.position.z);
        
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > agent.stoppingDistance) 
            state = EnemyState.CHASE;

        else if(distance <= agent.stoppingDistance) 
        {
            state = EnemyState.ATTACK;
        }
        
    }

    void ChaseState() 
    {
        rb.isKinematic = false;
        capsColl.isTrigger = false;

        if(agent.enabled == true)
            agent.SetDestination(target.position); 
    }

    void AttackState() 
    {
        FaceTarget(); 

        rb.isKinematic = true;
        capsColl.isTrigger = true;
        
        Debug.Log("Attacking");
        if(target.TryGetComponent<PlayerStats>(out var targetStats)) 
        {
            combat.Attack(targetStats);

            // Play attack animation
        }
    }

    // public void GetKnockedBack(Vector3 force)
    // {
    //     StartCoroutine(ApplyKnockback(force));
    // }

    // private IEnumerator ApplyKnockback(Vector3 force)
    // {
    //     // previousState = state;
    //     state = EnemyState.KNOCKEDBACK;

    //     yield return new WaitForSeconds(.25f);

    //     // yield return null;
    //     // Debug.Log("Have waited 1 frame...");
        
    //     agent.enabled = false;
    //     rb.useGravity = true;
    //     rb.AddForce(force);
    //     Debug.Log("agent enabled, gravity one, add force");

    //     yield return new WaitForFixedUpdate();
    //     yield return new WaitUntil(() => rb.velocity.magnitude < stillTreshold);
    //     yield return new WaitForSeconds(.25f);
    //     Debug.Log("WaitForFixedUpdate, WaitUntil, WaitForSeconds");


    //     rb.useGravity = false;
    //     agent.Warp(transform.position);
    //     agent.enabled = true;
    //     Debug.Log("gravity off, warp, agent enable");

    //     yield return new WaitForSeconds(.25f);

    //     // yield  return null;
    // }
}
