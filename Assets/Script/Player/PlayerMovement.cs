using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private CharacterController controller;
    public Quaternion targetRotation; // This is for the first movement method
    public float rotationSpeed; // Set it around 450. This is also for the first movement method
    public float movementSpeed; 

    void FixedUpdate()
    {
        MovePlayer2();
    }

    private void MovePlayer() // move the player and rotate the player based on the movement input
    {
       
        Vector3 input = new Vector3(Input.GetAxisRaw(Tags.HORIZONTAL), 0f, Input.GetAxisRaw(Tags.VERTICAL));
        if(input != Vector3.zero) 
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= movementSpeed * Time.deltaTime;
        motion += Vector3.up * -8;
        controller.Move(motion);
    }

    private void MovePlayer2() // only moves the player without rotation
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(Tags.HORIZONTAL), 0f, Input.GetAxisRaw(Tags.VERTICAL));
        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= movementSpeed * Time.deltaTime;
        motion += Vector3.up * -8;
        controller.Move(motion);
    }
}

