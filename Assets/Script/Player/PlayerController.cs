using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private TopDownMovement movement;
    public CharacterStats stats;
    public bool isIdle, isWalking, isRunning;
    public float enemy_attackRangeRadius, enemy_dashRadius;

    void Awake()
    {
        movement = GetComponent<TopDownMovement>();
        stats = GetComponent<CharacterStats>();
    }

    void Start()
    {
        PlayerManager.instance.state = PlayerManager.PlayerState.IDLE;
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
                if (movement.controller.velocity.sqrMagnitude < stats.walkSpeed.ReturnBaseValue())
                {
                    PlayerManager.instance.state = PlayerManager.PlayerState.IDLE;
                } 

                break;
            default:
                break;
        }
    }
}
