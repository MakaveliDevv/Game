using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public CharacterController controller;
    private InputHandler input;
    [SerializeField] private Camera cam;
    
    [HideInInspector] public Stat stat;

    public float verticalVelocity;
    public float gravity;

    void Awake() 
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputHandler>();
    }

    void Update() 
    {
        var targetVector = new Vector3(input.InputVector.x, 0, input.InputVector.y);
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
        Ray ray = cam.ScreenPointToRay(input.MousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo)) 
        {
            var target = hitInfo.point;
            target.y = transform.position.y;

            transform.LookAt(target);
        }
    }

    private void Gravity(ref Vector3 vector3) 
    {
        verticalVelocity -= gravity * Time.deltaTime;
        vector3.y = verticalVelocity * Time.deltaTime;
    }
}