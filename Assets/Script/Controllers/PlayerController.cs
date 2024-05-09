using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private TopDownMovement movement;
    [SerializeField] private PlayerAnimator anim;
    [SerializeField] private AbilityInput abilityInput;
    private CharacterStats stats;
    public bool isIdle, isWalking, isRunning;

    void Awake()
    {
        movement = GetComponent<TopDownMovement>();
        anim = GetComponent<PlayerAnimator>();
        stats = GetComponent<CharacterStats>();
        abilityInput = GetComponent<AbilityInput>();
    }

    void Start()
    {
        PlayerManager.instance.state = PlayerManager.PlayerState.IDLE;
        PlayerIdle();
    }
    

    void Update()
    {
        switch (PlayerManager.instance.state)
        {
            case PlayerManager.PlayerState.IDLE:
                if (movement.controller.velocity.sqrMagnitude > 0.01f)
                    PlayerManager.instance.state = PlayerManager.PlayerState.WALK; 

                break;

            case PlayerManager.PlayerState.WALK:
                PlayerWalk();          
                      
                if (movement.controller.velocity.sqrMagnitude < stats.walkSpeed.ReturnBaseValue())
                {
                    PlayerManager.instance.state = PlayerManager.PlayerState.IDLE;
                    PlayerIdle();
                } 

                break;

                // Need to find a way to be able to proceed walking after the character dashed

                case PlayerManager.PlayerState.DASH:
                    // Stop the walk anim

                    // anim.Walk(false);

                    // if(!abilityInput.sideStep) 
                    // {
                    //     PlayerManager.instance.state = PlayerManager.PlayerState.WALK;
                    // }
                    
                    // movement.stat = stats.dash;

                break;
            
            default:
                break;
        }
    }

    void PlayerIdle()
    {
        movement.stat = stats.walkSpeed;
        // movement.isMoving = false;

        isWalking = false;
        isRunning = false;

        anim.Idle(true);
        anim.Walk(false);
        // anim.Run(false);

        isIdle = true;
    }

    void PlayerWalk()
    {
        movement.stat = stats.walkSpeed;

        isIdle = false;
        isRunning = false;

        anim.Idle(false);
        anim.Walk(true);
        // anim.Run(false);

        isWalking = true;
    }

    void PlayerRun()
    {
        movement.stat = stats.runSpeed;

        isIdle = false;
        isWalking = false;

        anim.Idle(false);
        anim.Walk(false);
        anim.Run(true);

        isRunning = true;
    }
}
