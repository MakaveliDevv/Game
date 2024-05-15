using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/* This handles the behaviour of the Enemy */

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))] 
public class EnemyController : MonoBehaviour
{
    public enum EnemyState { CHASE, CLOSE_ATTACK, KNOCKEDBACK, RANGE_ATTACK, DASH_ATTACK, IDLE }
    public enum EnemyType { RANGE_ATTACKER, CLOSE_COMBAT, DASH_COMBAT }
    public EnemyState state;
    [SerializeField] private EnemyType typeState;
    // [SerializeField] private EnemyState previousState;

    public Transform target; // Player
    private PlayerController playerContr;
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats stats;
    private Rigidbody rb;
    private CapsuleCollider capsColl;
    [Range(0.00f, .1f)] [SerializeField] private float stillTreshold = .05f;



    // new stuff
    [SerializeField] private float distanceBehindPlayer;
    [SerializeField] private float rotationSpeed = 20f;

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
        FaceTarget(); 

        switch (state)
        {
            case EnemyState.CHASE:
                ChaseState();
                               
            break;

            case EnemyState.CLOSE_ATTACK:
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Movement() 
    {
        transform.position = new(transform.position.x, 0f, transform.position.z);
        agent.transform.position = new(transform.position.x, transform.position.y, transform.position.z);

        float distance = Vector3.Distance(target.position, transform.position);
     
        if (distance > agent.stoppingDistance)
        {
            state = EnemyState.CHASE;

            if (distance <= agent.stoppingDistance && typeState == EnemyType.CLOSE_COMBAT)
            {
                state = EnemyState.CLOSE_ATTACK;
            }
            else if (distance > agent.stoppingDistance && distance <= playerContr.enemy_attackRangeRadius && typeState == EnemyType.RANGE_ATTACKER)
            {
                state = EnemyState.RANGE_ATTACK;
            }
            else if (distance > agent.stoppingDistance && distance <= playerContr.enemy_dashRadius && typeState == EnemyType.DASH_COMBAT)
            {
                state = EnemyState.DASH_ATTACK;
            }
        }
    } 
     // private IEnumerator DashTowardsPosition()
    // {
    //     isDashing = true;

    //     // Disable NavMeshAgent
    //     agent.enabled = false;

    //     // Enable CapsuleCollider trigger to prevent collisions during dash
    //     capsColl.isTrigger = true;

    //     // Calculate the dash direction towards the target position
    //     Vector3 dashDirection = (target.position - transform.position).normalized;

    //     // Calculate the position behind the player
    //     Vector3 targetDashPosition = target.position + dashDirection * distanceBehindPlayer;

    //     // Calculate the distance to move during the dash
    //     float distanceToMove = Vector3.Distance(transform.position, targetDashPosition);

    //     // Calculate the duration based on the desired speed
    //     float dashDuration = distanceToMove / dashSpeed;

    //     // Start position of the dash
    //     Vector3 startPosition = transform.position;

    //     // Time elapsed during the dash
    //     float elapsedTime = 0f;

    //     // Dash movement loop
    //     while (elapsedTime < dashDuration)
    //     {
    //         // Interpolate the enemy's position towards the target position
    //         transform.position = Vector3.Lerp(startPosition, targetDashPosition, elapsedTime / dashDuration);

    //         // Update elapsed time
    //         elapsedTime += Time.deltaTime;

    //         yield return null;
    //     }

    //     // Ensure the enemy reaches the target position
    //     transform.position = targetDashPosition;

    //     // Optional delay before allowing the enemy to move or dash again
    //     yield return new WaitForSeconds(waitDuration);

    //     // Reset CapsuleCollider properties
    //     capsColl.isTrigger = false; // Re-enable collision

    //     // Warp agent to the current position
    //     agent.Warp(transform.position);
    //     Debug.Log("Agent Warp");

    //     // Re-enable NavMeshAgent
    //     agent.enabled = true;

    //     isDashing = false;
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
