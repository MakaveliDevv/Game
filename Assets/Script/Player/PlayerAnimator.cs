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
    
    public void Walk(bool walk) 
    {
        animator.SetBool("Walk", walk);
    }

    public void Run(bool run) 
    {
        animator.SetBool("Run", run);
    }
}
