using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Weapon wpn;
    private WeaponPickup wpnPickup;
    public Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float aimTreshHold = 15f;

    void Awake()
    {
        wpnPickup = GetComponent<WeaponPickup>();
        cam = Camera.main;
        firePoint = transform.GetChild(1);
    }

    void Start() 
    {
        wpn = wpnPickup.weapon;
        wpn.nextTimeToFire = 0;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time >= wpn.nextTimeToFire)
        {
            wpn.nextTimeToFire = Time.time + 1f/wpn.fireRate;
            Shoot();
        }
    }

    private void RayShoot() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = (targetPosition - firePoint.position).normalized;
            direction.y = 0;

            GameObject bulletInstance = Instantiate(wpn.bullet, firePoint.position, Quaternion.identity);
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.velocity = direction * wpn.bulletVelocity;
        }
    }

    private void Shoot() 
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        Vector3 direction = (mousePosition - firePoint.transform.position).normalized;

        direction.y = 0;
        direction.Normalize();
        
        GameObject bulletInstance = Instantiate(wpn.bullet, firePoint.transform.position, Quaternion.identity);
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = wpn.bulletVelocity * direction;
    }
    
    // private InputHandler FindComponentInMainParent<InputHandler>() 
    // {
    //     Transform parent = transform.parent;
    //     while(parent.parent != null)
    //         parent = parent.parent;

    //     return parent.GetComponent<InputHandler>();
    // }
}
