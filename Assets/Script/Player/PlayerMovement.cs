using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public Stat currentSpeed;
    // public float currentSpeed;
    // public float walkSpeed, runSpeed;


    public float verticalVelocity;
    public float gravity; // Set the value in the inspector

    // public bool isMoving;

    void Awake() 
    {
        controller = GetComponent<CharacterController>();
    }
    
    public void Move(Vector3 vector3) 
    {
        vector3 *= (Mathf.Abs(vector3.x) == 1 && Mathf.Abs(vector3.z) == 1) ? .7f : 1; // This prevent faster movement when moving diagonally
        vector3 *= currentSpeed.GetValue() * Time.deltaTime;
        vector3.y += verticalVelocity * Time.deltaTime;
        controller.Move(vector3); 
             
        Gravity(vector3);
    }

    private void Gravity(Vector3 vector3) 
    {
        verticalVelocity -= gravity * Time.deltaTime;
        vector3.y = verticalVelocity * Time.deltaTime;
    }
}

