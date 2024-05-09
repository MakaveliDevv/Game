using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityInput : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerController playerContr;
    public float maxDistance = 5f; 
    public float sidestepSpeed = 10f;
    public float cooldownTime = 1f;
    public bool sideStep, cooldown;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerContr = GetComponent<PlayerController>();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !cooldown)
        {
            PlayerManager.instance.previousState = PlayerManager.instance.state;
            PlayerManager.instance.state = PlayerManager.PlayerState.DASH;
        }

        // if(Input.GetKeyUp(KeyCode.Space)) 
        // {
        //     PlayerManager.instance.state = PlayerManager.instance.previousState;
        //     // sideStep = false;
        // } 
    }

    public void SideStep() 
    {   
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y; // Distance from the camera to the game world

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = (worldPosition - transform.position).normalized;
        Vector3 targetPosition = transform.position + direction * maxDistance;

        targetPosition.y = transform.position.y;
        StartCoroutine(MoveToTarget(targetPosition));
        sideStep = true;

        StartCoroutine(AbilityCooldown());
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            moveDirection.y = 0f; 
            // characterController.Move(moveDirection * sidestepSpeed * Time.deltaTime);
            characterController.Move(sidestepSpeed * Time.deltaTime * moveDirection);
            yield return null;
        }
    }

    IEnumerator AbilityCooldown()
    {
        cooldown = true;
        sideStep = false;
        PlayerManager.instance.state = PlayerManager.instance.previousState;

        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }
}
