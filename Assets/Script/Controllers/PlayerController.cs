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
                if (movement.controller.velocity.sqrMagnitude > 0.01f && !abilityInput.sideStep)
                {
                    PlayerManager.instance.state = PlayerManager.PlayerState.WALK;
                    PlayerWalk();                
                } 

                break;

            case PlayerManager.PlayerState.WALK:
                if (movement.controller.velocity.sqrMagnitude < stats.walkSpeed.ReturnBaseValue() && !abilityInput.sideStep)
                {
                    PlayerManager.instance.state = PlayerManager.PlayerState.IDLE;
                    PlayerIdle();
                } 


                break;

                case PlayerManager.PlayerState.DASH:
                    Debug.Log("Dash State");
                    abilityInput.SideStep();

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
        anim.Run(false);

        isIdle = true;
    }

    void PlayerWalk()
    {
        movement.stat = stats.walkSpeed;

        isIdle = false;
        isRunning = false;

        anim.Idle(false);
        anim.Walk(true);
        anim.Run(false);

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
