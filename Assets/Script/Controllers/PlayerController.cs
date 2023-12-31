using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { IDLE, WALK, RUN, DASH, ATTACK }

public class PlayerController : MonoBehaviour
{
    private PlayerState state;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerAnimator anim;
    private CharacterStats stats;
    private Vector3 movementInput;
    public bool isIdle, isWalking, isRunning;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<PlayerAnimator>();
        stats = GetComponent<CharacterStats>();
    }

    void Start()
    {
        state = PlayerState.IDLE;
        player.currentSpeed = stats.walkSpeed;

        isWalking = false;
        isRunning = false;
        isIdle = false;
    }
    

    void Update()
    {
        // Debug.Log(player.controller.velocity.sqrMagnitude);
        movementInput = new Vector3(Input.GetAxisRaw(Tags.HORIZONTAL), 0f, Input.GetAxisRaw(Tags.VERTICAL));

        // Check for state transitions
        switch (state)
        {
            case PlayerState.IDLE:
                // Check if the player is going to walk or run
                if (player.controller.velocity.sqrMagnitude > 0.01f)
                {
                    state = PlayerState.WALK;
                    PlayerWalk();                
                } else if(player.controller.velocity.sqrMagnitude > 0.01f && Input.GetKeyDown(KeyCode.LeftShift)) 
                {
                    state = PlayerState.RUN;
                    PlayerRun();
                }

                break;

            case PlayerState.WALK:
                // Check if the player is going to run or idle
                if (player.controller.velocity.sqrMagnitude < stats.walkSpeed.ReturnBaseValue())
                {
                    state = PlayerState.IDLE;
                    PlayerIdle();

                } else if(Input.GetKeyDown(KeyCode.LeftShift)) 
                {
                    state = PlayerState.RUN;
                    PlayerRun();
                }

                break;

            case PlayerState.RUN:
                if(player.controller.velocity.sqrMagnitude > stats.walkSpeed.ReturnBaseValue() && Input.GetKeyUp(KeyCode.LeftShift)) 
                {
                    state = PlayerState.WALK;
                    PlayerWalk();
                } 

                break;
            default:
                break;
        }
    }

    void FixedUpdate() 
    {
        player.Move(movementInput);
    }

    void PlayerIdle()
    {
        player.currentSpeed = stats.walkSpeed;
        isWalking = false;
        isRunning = false;

        anim.Idle(true);
        anim.Walk(false);
        anim.Run(false);

        isIdle = true;
    }

    void PlayerWalk()
    {
        player.currentSpeed = stats.walkSpeed;

        isIdle = false;
        isRunning = false;

        anim.Idle(false);
        anim.Walk(true);
        anim.Run(false);

        isWalking = true;
    }

    void PlayerRun()
    {
        player.currentSpeed = stats.runSpeed;

        isIdle = false;
        isWalking = false;

        anim.Idle(false);
        anim.Walk(false);
        anim.Run(true);

        isRunning = true;
    }
}
