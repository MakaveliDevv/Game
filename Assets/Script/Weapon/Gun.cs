using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Weapon wpn;
    private WeaponPickup wpnPickup;
    public Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private InputHandler input;

    private float rotationSpeed = 5f;
    private float aimTreshHold = 15f;

    void Awake()
    {
        wpnPickup = GetComponent<WeaponPickup>();
        cam = Camera.main;
        input = FindFirstObjectByType<InputHandler>().GetComponent<InputHandler>();
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
            AimAndShoot();
        }
    }

    // private InputHandler FindComponentInMainParent<InputHandler>() 
    // {
    //     Transform parent = transform.parent;
    //     while(parent.parent != null)
    //         parent = parent.parent;

    //     return parent.GetComponent<InputHandler>();
    // }

    void AimAndShoot()
    {
        Ray ray = cam.ScreenPointToRay(input.MousePosition);
        // bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, wpn.range, layerMask);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: wpn.range, layerMask)) 
        {
            if(hitInfo.collider.gameObject.layer == layerMask) 
            {
                Vector3 targetDirection = hitInfo.point - firePoint.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                firePoint.transform.rotation = Quaternion.RotateTowards(firePoint.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if(Quaternion.Angle(firePoint.transform.rotation, targetRotation) < aimTreshHold) 
                {
                    Shoot();
                }
            }
        }

    }

    private void Shoot() 
    {
        // Instantiate bullet
        GameObject bulletInstance = Instantiate(wpn.bullet, firePoint.transform.position, Quaternion.identity);

        // Calculate direction towards mouse position
        Vector3 direction = cam.ScreenToWorldPoint(input.MousePosition) - firePoint.transform.position;

        // Set bullet direction
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        // rb.AddForce(direction * wpn.bulletVelocity);s
        rb.velocity = direction * wpn.bulletVelocity;
        
    }
}
