using UnityEngine;
using UnityEngine.EventSystems;

public class TopDownMovement : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;
    [SerializeField] private PlayerAnimator playerAnim;
    private Camera cam;    
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravity = 5f;
    public bool ableToLookAround;

    public bool moveForward, moveBackward, moveLeft, moveRight, idle;
    public bool isMoving;
    public bool canMove, canLook, canPlayAnim;

    void Awake() 
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<PlayerAnimator>();
        cam = Camera.main;
        idle = true;
        canMove = true;
        canLook = true;
        canPlayAnim = true;
    }

    void FixedUpdate()
    {
        Vector3 targetVector = GetMovementInput(); 
        MoveTowardTarget(targetVector);
        RotateTowardMouseVector();
    }

    void Update() 
    {
        if(controller.velocity.sqrMagnitude < 0.01f) 
        {
            // Animation
            playerAnim.Idle(true);
            isMoving = false;
        }
        else if(controller.velocity.sqrMagnitude > 0.01f)
        {
            isMoving = true;
        }

        if(GameManager.instance.isTimerRunning) 
        {
            canMove = true;
            canLook = true;
            canPlayAnim = true;
        } 
        else 
        {
            canMove = false;
            canLook = false;
            canPlayAnim = false;
        }

        MovementAnimation();
    }

    private void MovementAnimation()
    {
        if(canPlayAnim) 
        {
            // Move forward
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveForward = true;

                // Animation
                playerAnim.MoveForward(true);
            }

            if (Input.GetKeyUp(KeyCode.W))
            { 
                moveForward = false;

                // Animation
                playerAnim.MoveForward(false);
                playerAnim.Idle(true);    
            }
            
            // Move backward
            if (Input.GetKeyDown(KeyCode.S))
            {
                moveBackward = true;

                // Animation
                playerAnim.MoveBackward(true);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                moveBackward = false;

                // Animation
                playerAnim.MoveBackward(false);
                playerAnim.Idle(true);
            }

            // Move left
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveLeft = true;
                playerAnim.MoveBackward(true);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                moveLeft = false;

                // Animation
                playerAnim.MoveBackward(false);
                playerAnim.Idle(true);
            }

            // Move right
            if (Input.GetKeyDown(KeyCode.D))
            {
                moveRight = true;

                // Animation
                playerAnim.MoveForward(true);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                moveRight = false;

                // Animation
                playerAnim.MoveForward(false);
                playerAnim.Idle(true);
            }
        }
    }

    private Vector3 GetMovementInput()
    {
        float x = 0;
        float z = 0;

        if (moveForward)
        {
            z += 1;
        }

        if (moveBackward)
        {
            z -= 1;
        }

        if (moveLeft)
        {
            x -= 1;
        }

        if (moveRight)
        {
            x += 1;

        }

        return new Vector3(x, 0, z).normalized;
    }


    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        // If something, then move
        if(canMove)
        {
            PlayerStats stat = GetComponent<PlayerStats>();
            float speed = stat.walkSpeed.GetValue();    
                
            targetVector = Quaternion.Euler(0, cam.gameObject.transform.eulerAngles.y, 0) * targetVector;
            targetVector *= (Mathf.Abs(targetVector.x) == 1 && Mathf.Abs(targetVector.z) == 1) ? .7f : 1; // Prevent quicker movement when moving diagonally
            Vector3 targetPosition = targetVector * speed;

            Gravity(ref targetPosition);
            controller.Move(targetPosition * Time.deltaTime);
        }

        return targetVector;
    }

    private void RotateTowardMouseVector()
    {
        // if(canLook) 
        // {
        //     Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        //     mousePosition.y = transform.position.y;
        //     transform.LookAt(mousePosition);
        // }

        if (canLook)
        {
            // Check if mouse is over UI
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Convert mouse position to world coordinates
                Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
                mousePosition.y = transform.position.y; // Keep the same y-coordinate as the player

                // Rotate towards the mouse position
                transform.LookAt(mousePosition);
            }
        }
    }

    private void Gravity(ref Vector3 vector3) 
    {
        verticalVelocity -= gravity * Time.deltaTime;
        vector3.y = verticalVelocity * Time.deltaTime;
    }
}