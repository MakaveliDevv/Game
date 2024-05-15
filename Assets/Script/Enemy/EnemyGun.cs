using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private EnemyWeapon weapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject weaponSlot;
    // [SerializeField] private float rotationSpeed = 5f;
    // [SerializeField] private float aimTreshHold = 15f;

    private EnemyController enemyController;

    void Awake() 
    {
        enemyController = weaponSlot.GetComponentInParent<EnemyController>();
    }

    void Update() 
    {
        if(enemyController.state == EnemyController.EnemyState.COMBAT 
        && enemyController.attackState == EnemyController.AttackState.RANGE_ATTACK) 
        {
            if (Time.time >= weapon.nextTimeToFire)
            {
                weapon.nextTimeToFire = Time.time + 1f / weapon.fireRate;
                EnemyShoot();
            }
        }
    }

    void EnemyShoot()
    {
        GameObject bullet = Instantiate(weapon.bullet, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = weapon.bulletVelocity * (enemyController.target.position - firePoint.position).normalized;
    }

}
