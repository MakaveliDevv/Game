using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;

    void Awake() 
    {
        animator = GetComponent<Animator>();
    }
    
    public void Move(bool move) 
    {
        animator.SetBool("Move", move);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Shoot() 
    {
        animator.SetTrigger("Shoot");
    }

    public void ResetShoot() 
    {
        animator.ResetTrigger("Shoot");
    }
    
    public void Death(bool death) 
    {
        animator.SetBool("Death", death);
    }

    public void ShootBool(bool shoot) 
    {
        animator.SetBool("ShootBool", shoot);
    }
}
