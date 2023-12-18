// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public enum PlayerStatee { IDLE, WALK, RUN, DASH, ATTACK}

// public class PlayerControllerr : MonoBehaviour
// {
//     private PlayerState state;

//     private PlayerMovement player;
//     [SerializeField] private PlayerAnimator anim;
//     private Vector3 movementInput;

//     public float walkSpeed, runSpeed;    

//     void Awake() 
//     {
//         player = GetComponent<PlayerMovement>();
//         anim = GetComponent<PlayerAnimator>();
//     } 

//     void Start()
// {
//     state = PlayerState.IDLE;
//     player.currentSpeed = walkSpeed;

//     // Set initial animation state based on the initial player state
//     switch (state)
//     {
//         case PlayerState.IDLE:
//             PlayerIdle();
//             break;
//         case PlayerState.WALK:
//             PlayerWalk();
//             break;
//         case PlayerState.RUN:
//             PlayerRun();
//             break;
//         // Add cases for other states if needed
//         default:
//             break;
//     }
// }

// void FixedUpdate()
// {
//     movementInput = new Vector3(Input.GetAxisRaw(Tags.HORIZONTAL), 0f, Input.GetAxisRaw(Tags.VERTICAL));
//     player.Move(movementInput);

//     // Check for state transitions
//     switch (state)
//     {
//         case PlayerState.IDLE:
//             if (player.controller.velocity.sqrMagnitude > 0.01f)
//             {
//                 state = PlayerState.WALK;
//                 PlayerWalk();
//             }
//             break;
//         case PlayerState.WALK:
//             if (player.controller.velocity.sqrMagnitude < walkSpeed)
//             {
//                 state = PlayerState.IDLE;
//                 PlayerIdle();
//             }
//             // Add other conditions for transitioning to RUN if needed
//             break;
//         case PlayerState.RUN:
//             // Add conditions for transitioning to WALK or IDLE if needed
//             break;
//         // Add cases for other states if needed
//         default:
//             break;
//     }
// }

// void PlayerIdle()
// {
//     anim.Idle(true);
//     anim.Walk(false);
//     anim.Run(false);
// }

// void PlayerWalk()
// {
//     anim.Idle(false);
//     anim.Walk(true);
//     anim.Run(false);
// }

// void PlayerRun()
// {
//     anim.Idle(false);
//     anim.Walk(false);
//     anim.Run(true);
// }

// }
