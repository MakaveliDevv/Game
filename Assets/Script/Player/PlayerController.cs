using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { IDLE, WALK, RUN, DASH, ATTACK }

public class PlayerController : MonoBehaviour
{
    private PlayerState state;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerAnimator anim;
    private Vector3 movementInput;

    public bool isIdle, isWalking, isRunning;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<PlayerAnimator>();
    }

    void Start()
    {
        state = PlayerState.IDLE;
        player.currentSpeed = player.walkSpeed;

        isWalking = false;
        isRunning = false;
        isIdle = false;
    }

    void Update()
    {
        movementInput = new Vector3(Input.GetAxisRaw(Tags.HORIZONTAL), 0f, Input.GetAxisRaw(Tags.VERTICAL));
        // Debug.Log(player.controller.velocity.sqrMagnitude);
    }

    // void FixedUpdate()
    // {
    //     player.Move(movementInput);

    //     // Check for state transitions
    //     switch (state)
    //     {
    //         case PlayerState.IDLE:
    //             // Check if the player is going to walk or run
    //             if (player.controller.velocity.sqrMagnitude > 0.01f)
    //             {
    //                 PlayerWalk();                
    //                 state = PlayerState.WALK;
    //             } else if(player.controller.velocity.sqrMagnitude > 0.01f && Input.GetKeyDown(KeyCode.LeftShift)) 
    //             {
    //                 PlayerRun();
    //                 state = PlayerState.RUN;
    //             }

    //             break;

    //         case PlayerState.WALK:
    //             // Check if the player is going to run or idle
    //             if (player.controller.velocity.sqrMagnitude < player.walkSpeed)
    //             {
    //                 PlayerIdle();
    //                 state = PlayerState.IDLE;

    //             } else if(Input.GetKeyDown(KeyCode.LeftShift)) 
    //             {
    //                 PlayerRun();
    //                 state = PlayerState.RUN;
    //             }

    //             break;

    //         case PlayerState.RUN:
    //             if(player.controller.velocity.sqrMagnitude > player.walkSpeed && Input.GetKeyUp(KeyCode.LeftShift)) 
    //             {
    //                 PlayerWalk();
    //                 state = PlayerState.WALK;
    //             } 


    //             break;
    //         default:
    //             break;
    //     }
    // }

    void FixedUpdate()
    {
        player.Move(movementInput);

        // Check for state transitions
        switch (state)
        {
            case PlayerState.IDLE:
                if (player.controller.velocity.sqrMagnitude > 0.01f)
                {
                    PlayerWalk();
                    state = PlayerState.WALK;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerRun();
                    state = PlayerState.RUN;
                }
                break;

            case PlayerState.WALK:
                if (player.controller.velocity.sqrMagnitude < player.walkSpeed)
                {
                    PlayerIdle();
                    state = PlayerState.IDLE;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerRun();
                    state = PlayerState.RUN;
                }
                break;

            case PlayerState.RUN:
                if (player.controller.velocity.sqrMagnitude < player.runSpeed && !Input.GetKeyUp(KeyCode.LeftShift))
                {
                    PlayerWalk();
                    state = PlayerState.WALK;
                }
                break;

            default:
                break;
        }
    }


    void PlayerIdle()
    {
        player.currentSpeed = player.walkSpeed;
        isWalking = false;
        isRunning = false;

        anim.Idle(true);
        anim.Walk(false);
        anim.Run(false);

        isIdle = true;
    }

    void PlayerWalk()
    {
        player.currentSpeed = player.walkSpeed;

        isIdle = false;
        isRunning = false;

        anim.Idle(false);
        anim.Walk(true);
        anim.Run(false);

        isWalking = true;
    }

    void PlayerRun()
    {
        player.currentSpeed = player.runSpeed;

        isIdle = false;
        isWalking = false;

        anim.Idle(false);
        anim.Walk(false);
        anim.Run(true);

        isRunning = true;
    }
}
