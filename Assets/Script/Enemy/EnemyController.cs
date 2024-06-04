using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/* This handles the behaviour of the Enemy */

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))] 
public class EnemyController : MonoBehaviour
{
    // States
    public enum State { CHASE, COMBAT, KNOCKEDBACK, IDLE }
    public enum AttackState { CLOSE_COMBAT, RANGE_ATTACK, DASH_ATTACK, IDLE }
    public enum EnemyType { RANGE, MELEE, DASHER }
    
    [Header("States")]
    [HideInInspector] public State state;
    [SerializeField] private EnemyType type;
    private AttackState attackState;

    // Player stuff
    private Transform target; // Player
    private PlayerController playerContr;

    // Enemy stuff
    [SerializeField] private GameObject leftPalm, rightPalm;
    private EnemyAnimator animator;
    private EnemyGun gun;
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats stats;
    // private Rigidbody rb;
    // private CapsuleCollider capsColl;


    private SphereCollider leftPalmCol;
    private Rigidbody leftPalmRb;

    private SphereCollider rightPalmCol;
    private Rigidbody rightPalmRb;

    // Bool
    [SerializeField] private bool dashing;
    public bool shooting;

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
        
        gun = GetComponentInChildren<EnemyGun>();

        animator = GetComponent<EnemyAnimator>();
        attackState = AttackState.IDLE;

        // Left palm
        leftPalmCol = leftPalm.GetComponent<SphereCollider>();
        leftPalmRb = leftPalm.GetComponent<Rigidbody>();

        // Right palm
        rightPalmCol = rightPalm.GetComponent<SphereCollider>();
        rightPalmRb = rightPalm.GetComponent<Rigidbody>();

    }

    void Start() 
    {
        state = State.CHASE;
        attackState = AttackState.IDLE;
        agent.speed = stats.walkSpeed.ReturnBaseValue();
    }

    void FixedUpdate() 
    {
        Movement();
    }

    void Update()
    {        
        FaceTarget(); 
    	DefaultStateSwitch();
        AttackStateSwitch();
    }   

    private void AttackStateSwitch() 
    {
        switch(attackState)
        {
            case AttackState.CLOSE_COMBAT:
                // Play attack animation
                animator.Move(false);
                animator.Attack();
    	        CloseCombat();

            break;

            case AttackState.DASH_ATTACK:
                // DashAttack();

            break;

            case AttackState.RANGE_ATTACK:
                RangeAttack();

            break;
        }
    }

    private void DefaultStateSwitch() 
    {
        switch (state)
        {
            case State.CHASE:
                Chase();
                               
            break;

            case State.COMBAT:
                if(type == EnemyType.MELEE) 
                    attackState = AttackState.CLOSE_COMBAT;
                
                else if(type == EnemyType.DASHER)
                    attackState = AttackState.DASH_ATTACK;
                
                else if(type == EnemyType.RANGE) 
                    attackState = AttackState.RANGE_ATTACK;
                
                               
            break;

            case State.KNOCKEDBACK:
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
        if (agent.pathPending)
            return;
   
        transform.position = new(transform.position.x, 0f, transform.position.z);
        agent.transform.position = new(transform.position.x, transform.position.y, transform.position.z);

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance > agent.stoppingDistance && !dashing)
        {
            // Chase
            state = State.CHASE;
            // Debug.Log("Distance: " + distance + "StoppingDistance: " + agent.stoppingDistance);

        }

        // Check for close combat
        if (distance <= agent.stoppingDistance && type == EnemyType.MELEE)
        {
            // Debug.Log("Switch to combat state");
            state = State.COMBAT;
            attackState = AttackState.CLOSE_COMBAT;
        }
        else if (distance > agent.stoppingDistance && distance <= playerContr.enemy_attackRangeRadius && type == EnemyType.RANGE)
        {
            state = State.COMBAT;
            attackState = AttackState.RANGE_ATTACK;
        }
        // else if (distance > agent.stoppingDistance && distance <= playerContr.enemy_dashRadius && type == EnemyType.DASHER)
        // {
        //     state = State.COMBAT;
        //     attackState = AttackState.DASH_ATTACK;
        // }

        // if(agent.remainingDistance <= agent.stoppingDistance) 
        // {
        //     agent.isStopped = true;
        //     Debug.Log("Agent has reached the stopping point");
        // }
        // else
        //     agent.isStopped = false;

    } 

    // private void Movement() 
    // {
    //     if (agent.pathPending)
    //         return;
   
    //     transform.position = new(transform.position.x, 0f, transform.position.z);
    //     agent.transform.position = new(transform.position.x, transform.position.y, transform.position.z);

    //     float distance = Vector3.Distance(target.position, transform.position);

    //     if (distance > agent.stoppingDistance && !dashing)
    //     {
    //         // Chase
    //         state = State.CHASE;
    //         Debug.Log("Distance: " + distance + "StoppingDistance: " + agent.stoppingDistance);

    //         // Check for close combat
    //         if (agent.remainingDistance <= agent.stoppingDistance && type == EnemyType.MELEE)
    //         {
    //             Debug.Log("Switch to combat state");
    //             state = State.COMBAT;
    //             attackState = AttackState.CLOSE_COMBAT;
    //         }
    //         else if (distance > agent.stoppingDistance && distance <= playerContr.enemy_attackRangeRadius && type == EnemyType.RANGE)
    //         {
    //             state = State.COMBAT;
    //             attackState = AttackState.RANGE_ATTACK;
    //         }
    //         else if (distance > agent.stoppingDistance && distance <= playerContr.enemy_dashRadius && type == EnemyType.DASHER)
    //         {
    //             state = State.COMBAT;
    //             attackState = AttackState.DASH_ATTACK;
    //         }
    //     }

    //     if(agent.remainingDistance <= agent.stoppingDistance) 
    //     {
    //         agent.isStopped = true;
    //         Debug.Log("Agent has reached the stopping point");
    //     }
    //     else
    //         agent.isStopped = false;

    // } 

    private void Chase() 
    {
        attackState = AttackState.IDLE;
        shooting = false;
        
        // if(rb != null) rb.isKinematic = false;
        // if (capsColl != null) capsColl.isTrigger = false;

        if(leftPalmRb != null) leftPalmRb.isKinematic = false;
        if(leftPalmCol != null) leftPalmCol.isTrigger = false;

        if(rightPalmRb != null) rightPalmRb.isKinematic = false;
        if(rightPalmCol != null) rightPalmCol.isTrigger = false;

        if(agent.enabled == true)
        {
            agent.SetDestination(target.position);

            // animator.ResetShoot(); 
            animator.Move(true);
        }
    }

    private void CloseCombat() 
    {      
        leftPalmCol.isTrigger = true;
        leftPalmRb.isKinematic = true;

        rightPalmCol.isTrigger = true;
        rightPalmRb.isKinematic = true;


        // rb.isKinematic = true;
        // capsColl.isTrigger = true;
        
        if(target.TryGetComponent<PlayerStats>(out var targetStats)) 
        {
            combat.Attack(targetStats);
            Debug.Log("Attack method got executed!");

            // // Play attack animation
            // animator.Move(false);
            // animator.Attack();
        }
    }
    
    // Range attack
    private void RangeAttack() 
    {
        agent.velocity = new(0f, 0f, 0f);

        // Play attack animation
        animator.Move(false);
        animator.Shoot();

        Debug.Log("Player Hitt");
    }

    public void Shoot()
    {
        // Debug.Log("Shoot at the right frame");
        GameObject bullet = Instantiate(gun.weapon.bullet, gun.firePoint.position, gun.firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = gun.weapon.bulletVelocity * (target.position - gun.firePoint.position).normalized + new Vector3(0, 1f, 0);        
    }


    // Dash
    // private void DashAttack() 
    // {
    //     // Check if not already dashing
    //     if (!dashing)
    //     {
    //         // Set isDashing to true
    //         dashing = true;

    //         // Start dashing coroutine
    //         StartCoroutine(DashTowardsPosition());
    //     }
    // } 

    // private void DashAttack() 
    // {
    //     // Some dash movement
    //     StartCoroutine(DashTowardsPosition());

    //     // float distance = Vector3.Distance(target.position, transform.position);
    //     // if(distance > playerContr.enemy_dashRadius) 
    //     //     state = EnemyState.CHASE;
    
    // }

    // private IEnumerator DashTowardsPosition()
    // {
    //     agent.enabled = false;
        
    //     yield return new WaitForSeconds(.25f);

    //     agent.enabled = true;
    //     // Disable NavMeshAgent auto-update
    //     agent.autoBraking = true;
    //     agent.speed = 15f; // Adjust as needed
    //     agent.acceleration = 10f; // Adjust as needed

    //     rb.isKinematic = true;
    //     capsColl.isTrigger = true;

    //     // Calculate the target position behind the player
    //     Vector3 dashDirection = (target.position - transform.position).normalized;
    //     Vector3 targetDashPosition = target.position - dashDirection * distanceBehindPlayer;

    //     // Set the destination for NavMeshAgent
    //     agent.SetDestination(targetDashPosition);

    //     // Wait until the enemy reaches the destination
    //     while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
    //     {
    //         yield return null;
    //     }

    //     dashing = false;
    // }

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
