using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    #region Singleton
    public static WeaponManagement instance;
    void Awake()
    {
        instance = this;
    }
    #endregion 

    private GameObject weaponHolder;
    private GameObject defaultWeapon;
    
    private GameObject currentWeaponObject; // Keep track of the currently equipped weapon object
    private GameObject newWeaponObject;

    void Start()
    {
        weaponHolder = transform.GetChild(2).gameObject;
        defaultWeapon = weaponHolder.transform.GetChild(0).gameObject;
        currentWeaponObject = defaultWeapon;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            // Ensure the object has a WeaponPickup script
            WeaponPickup script = other.GetComponent<WeaponPickup>();
            if (script != null)
            {
                // Equip the weapon and get the instantiated weapon object
                newWeaponObject = script.Equip();

                // Set the position and rotation as needed
                newWeaponObject.transform.localPosition = Vector3.zero;
                newWeaponObject.transform.localRotation = Quaternion.identity;

                // Deactivate the default weapon
                defaultWeapon.SetActive(false);

                // Update the reference to the current weapon object
                currentWeaponObject = newWeaponObject;
            }
        }
    }
}
