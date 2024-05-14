using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/* This handles the behaviour of the Enemy */

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))] 
public class EnemyController : MonoBehaviour
{
    public enum EnemyState { CHASE, ATTACK, KNOCKEDBACK, RANGE_ATTACK, DASH_ATTACK, IDLE }
    public enum EnemyType { RANGE_ATTACKER, CLOSE_COMBAT, DASH_COMBAT }
    [SerializeField] private EnemyState state;
    [SerializeField] private EnemyType typeState;
    // [SerializeField] private EnemyState previousState;

    private Transform target; // Player
    private PlayerController playerContr;
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats stats;
    private Rigidbody rb;
    private CapsuleCollider capsColl;
    [Range(0.00f, .1f)] [SerializeField] private float stillTreshold = .05f;



    // new stuff
    [SerializeField] private float distanceBehindPlayer;

    void Awake() 
    {
        target = PlayerManager.instance.player.transform;
        playerContr = target.GetComponent<PlayerController>();

        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        
        rb = GetComponent<Rigidbody>();
        capsColl = GetComponent<CapsuleCollider>();

    }

    void Start() 
    {
        state = EnemyState.CHASE;
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

            case EnemyState.DASH_ATTACK:
                DashAttackState();
                
            break;

            case EnemyState.RANGE_ATTACK:
                RangeAttackState();
                
            break;

            case EnemyState.KNOCKEDBACK:
                Debug.Log("Knockback state");

            break;
            // default: 
        }
    }    

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Movement() 
    {
        transform.position = new(transform.position.x, 0f, transform.position.z);
        agent.transform.position = new(transform.position.x, transform.position.y, transform.position.z);

        float distance = Vector3.Distance(target.position, transform.position);
        
        if(distance > agent.stoppingDistance && state != EnemyState.RANGE_ATTACK
        && state != EnemyState.DASH_ATTACK)  
            state = EnemyState.CHASE;

        if(distance <= agent.stoppingDistance) 
            state = EnemyState.ATTACK;

        if(typeState == EnemyType.RANGE_ATTACKER && distance <= playerContr.enemy_attackRangeRadius) 
            state = EnemyState.RANGE_ATTACK;

        if(typeState == EnemyType.DASH_COMBAT && distance <= playerContr.enemy_dashRadius)
            state = EnemyState.DASH_ATTACK;  
    }


    // Check if the enemy is within the player's radius
    // if (distance <= playerContr.radius) 
    // {
    //     // Generate a random angle in radians
    //     float randomAngle = Random.Range(0f, Mathf.PI * 2); // Range from 0 to 2*PI radians
        
    //     // Calculate the x and y offsets using trigonometry
    //     float offsetX = Mathf.Cos(randomAngle) * Random.Range(playerContr.enemy_minOffset, playerContr.enemy_maxOffset);
    //     float offsetY = Mathf.Sin(randomAngle) * Random.Range(playerContr.enemy_minOffset, playerContr.enemy_maxOffset);
        
    //     // Calculate the attack position relative to the player
    //     Vector3 attackPosition = target.position + new Vector3(offsetX, 0f, offsetY);
    //     Debug.Log(attackPosition);
        
    //     // Check if the enemy is within the attack range
    //     if (Vector3.Distance(transform.position, attackPosition) <= playerContr.radius) 
    //         state = EnemyState.RANGE_ATTACK;
    // }
    // Check if the enemy is within the player's radius
    // if (distance <= playerContr.radius) 
    // {
    //     float randomOffset = Random.Range(playerContr.enemy_minOffset, playerContr.enemy_maxOffset);
    //     int sign = Random.Range(-1, 2);

    //     randomOffset *= sign;
        
    //     float attackDistanceRange = playerContr.radius + randomOffset;
        
    //     if (distance <= attackDistanceRange) 
    //         state = EnemyState.RANGE_ATTACK;
    // }

    private void ChaseState() 
    {
        rb.isKinematic = false;
        capsColl.isTrigger = false;

        if(agent.enabled == true)
            agent.SetDestination(target.position); 
    }

    private void AttackState() 
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

    private void RangeAttackState() 
    {
        agent.velocity = new(0, 0, 0);

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > playerContr.enemy_attackRangeRadius) 
            state = EnemyState.CHASE;

        // If the position of the enemy is larger than the radius of the player then move the enemy again
        Debug.Log("Enemy is attacking from a distance");   
    }

    private void DashAttackState() 
    {
        // Some dash movement
        StartCoroutine(DashTowardsPosition());

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > playerContr.enemy_dashRadius) 
            state = EnemyState.CHASE;
    
    }
    private IEnumerator DashTowardsPosition()
    {
        // Disable NavMeshAgent auto-update
        agent.autoBraking = false;
        agent.speed = 15f; // Adjust as needed
        agent.acceleration = 10f; // Adjust as needed

        rb.isKinematic = true;
        capsColl.isTrigger = true;

        // Calculate the target position behind the player
        Vector3 dashDirection = (target.position - transform.position).normalized;
        Vector3 targetDashPosition = target.position - dashDirection * distanceBehindPlayer;

        // Set the destination for NavMeshAgent
        agent.SetDestination(targetDashPosition);

        // Wait until the enemy reaches the destination
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        // Re-enable NavMeshAgent auto-update
        agent.autoBraking = true;
                
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
