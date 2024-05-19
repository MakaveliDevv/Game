using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Idle(bool idle) 
    {
        animator.SetBool("Idle", idle);
    }   
    
    public void MoveForward(bool moveForward) 
    {
        animator.SetBool("MoveForward", moveForward);
    }

    public void MoveBackward(bool moveBackward)
    {
        animator.SetBool("MoveBackward", moveBackward);
    }
    
    public void Death(bool death) 
    {
        animator.SetBool("Death", death);
    }
}
 