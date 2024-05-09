using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
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
            weapon = WeaponInventory.instance.ReturnWeapon();

            if(weapon != null) 
            {
                weapon.nextTimeToFire = 0;
            }
        }
    }

    void Update() 
    {
        StartCoroutine(EquipTimer());
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time >= weapon.nextTimeToFire)
        {
            weapon.nextTimeToFire = Time.time + 1f/weapon.fireRate;
            Shoot();
        }
    }

    private void Shoot() 
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        Vector3 direction = (mousePosition - transform.position).normalized;

        float minDirectionLength = .1f;
        if(direction.magnitude < minDirectionLength) 
            direction = Vector3.forward * weapon.bulletVelocity;

        else
            direction.Normalize();

        direction.y = 0;


        GameObject bulletInstance = Instantiate(weapon.bullet, firePoint.transform.position, Quaternion.identity);
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = weapon.bulletVelocity * direction;
    }

    private IEnumerator EquipTimer() 
    {
        if(weapon.equipped && !weapon.defaultWeapon) 
        {
            Debug.Log("Timer started");
            yield return new WaitForSeconds(weapon.equipTimer);

            weapon.equipped = false;
            
            WeaponInventory.instance.weapons.Clear();
            WeaponInventory.instance.weapons.Add(WeaponInventory.instance.defaultWeapon);
            
            WeaponInventory.instance.defaultWeaponSlot.gameObject.SetActive(true);           
            WeaponInventory.instance.defaultWeapon.equipped = true; 

            WeaponInventory.instance.previousWeapon = WeaponInventory.instance.currentWeapon;
            WeaponInventory.instance.currentWeapon = WeaponInventory.instance.defaultWeapon;

            Destroy(gameObject);
        }

        // yield break;
    }
}
