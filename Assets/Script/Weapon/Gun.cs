using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Weapon wpn;
    public Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float aimTreshHold = 15f;

    void Start() 
    {
        cam = Camera.main;
        firePoint = transform.GetChild(1);
        
        if(WeaponInventory.instance != null) 
        {
            wpn = WeaponInventory.instance.ReturnWeapon();

            if(wpn != null) 
            {
                wpn.nextTimeToFire = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time >= wpn.nextTimeToFire)
        {
            wpn.nextTimeToFire = Time.time + 1f/wpn.fireRate;
            Shoot();
        }
    }

    private void Shoot() 
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        Vector3 direction = (mousePosition - transform.position).normalized;

        float minDirectionLength = .1f;
        if(direction.magnitude < minDirectionLength) 
            direction = Vector3.forward * wpn.bulletVelocity;

        else
            direction.Normalize();

        direction.y = 0;


        GameObject bulletInstance = Instantiate(wpn.bullet, firePoint.transform.position, Quaternion.identity);
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = wpn.bulletVelocity * direction;
    }
}
