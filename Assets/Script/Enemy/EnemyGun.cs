using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public EnemyWeapon weapon;
    public Transform firePoint;
    [SerializeField] private LayerMask layerMask;
    // [SerializeField] private GameObject weaponSlot;
    // [SerializeField] private float rotationSpeed = 5f;
    // [SerializeField] private float aimTreshHold = 15f;

    // private EnemyController controller;

    void Awake() 
    {
        // controller = GetComponentInParent<EnemyController>();

        weapon.nextTimeToFire = 0;
    }


    // I MADE THIS BEFORE THE ANIMATION
    // NOW I FIRE THE BULLETS OFF AT A SPECIFIC KEY FRAME
    // MAYBE I CAN STILL MAKE USE OF THIS

    // void Update() 
    // {
    //     if(controller.state == EnemyController.State.COMBAT 
    //     && controller.attackState == EnemyController.AttackState.RANGE_ATTACK) 
    //     {
    //         if (!controller.shooting)
    //         {
    //             StartCoroutine(ShootCoroutine());
    //         }
    //     }
    // }


    // private IEnumerator ShootCoroutine()
    // {
    //     while(controller.state == EnemyController.State.COMBAT 
    //         && controller.attackState == EnemyController.AttackState.RANGE_ATTACK)
    //     {
    //         if (Time.time >= weapon.nextTimeToFire)
    //         {
    //             weapon.nextTimeToFire = Time.time + 1f / weapon.fireRate;
    //             // Shoot();

    //             controller.animator.Shoott("Shoot");
    //         }

    //         yield return null;  // Wait for the next frame
    //     }

    //     controller.shooting = false;
    // }

    // public void Shoot()
    // {
    //     GameObject bullet = Instantiate(weapon.bullet, firePoint.position, firePoint.rotation);
    //     Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //     rb.velocity = weapon.bulletVelocity * (controller.target.position - firePoint.position).normalized;
        
    //     controller.shooting = true;
    // }
    

}
