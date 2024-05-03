using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public CharacterController controller;
    private Camera cam;
    
    [HideInInspector] public Stat stat;

    public float verticalVelocity;
    public float gravity;

    void Awake() 
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update() 
    {
        var targetVector = new Vector3(Input.GetAxisRaw(Tags.HORIZONTAL), 0, Input.GetAxisRaw(Tags.VERTICAL));
        MoveTowardTarget(targetVector);
        RotateTowardMouseVector();
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = stat.ReturnBaseValue() * Time.deltaTime;
        targetVector = Quaternion.Euler(0, cam.gameObject.transform.eulerAngles.y, 0) * targetVector;
        
        targetVector *= (Mathf.Abs(targetVector.x) == 1 && Mathf.Abs(targetVector.z) == 1) ? .7f : 1; // Prevent quicker movement when moving diagonally
        var targetPosition = targetVector * speed;

        Gravity(ref targetPosition);
        controller.Move(targetPosition);

        return targetVector;
    }

    private void RotateTowardMouseVector()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        mousePosition.y = transform.position.y;
        transform.LookAt(mousePosition);
    }

    private void Gravity(ref Vector3 vector3) 
    {
        verticalVelocity -= gravity * Time.deltaTime;
        vector3.y = verticalVelocity * Time.deltaTime;
    }
}