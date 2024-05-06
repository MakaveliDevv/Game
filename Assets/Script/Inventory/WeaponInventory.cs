using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    #region Singleton
    public static WeaponInventory instance;
    void Awake() 
    {
        if(instance != null) 
        {
            Destroy(gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Ensure that this object persists between scenes
    }
    #endregion 

    // private Weapon weaponInstance; // This is for the default weapon
    private Weapon currentWeapon; // Keep track of the currently equipped weapon
    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();

    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;


    [SerializeField] private Weapon defaultWeapon;
    [HideInInspector] public GameObject defWeaponObj; 
    public List<Weapon> weapons = new();
    public int space = 1;
    
    void Start()
    {     
        GameObject weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot"); // Slot
    
        defWeaponObj = Instantiate(defaultWeapon.wpnObject, weaponSlot.transform.position, Quaternion.identity) as GameObject;
        defWeaponObj.transform.SetParent(weaponSlot.transform);
        defWeaponObj.name = defaultWeapon.Name;

        if (defWeaponObj != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon;
            weapons.Add(currentWeapon);

            // weaponInstance = currentWeapon; 
            // weaponInstance.weaponEquipped = true;
            
            // Call back methods
            // weaponPickupCallBack?.Invoke(weaponInstance);
            // weaponPickupUICallBack?.Invoke();

        }
    }

    public bool CanCollectWeapon()
    {
        return weapons.Count <= space;
    }


    public bool AddWeapon(Weapon _newWeapon)
    {
        if (!CanCollectWeapon())
            return false;

        Weapon previousWeapon = currentWeapon;

        // Update the currently equipped weapon
        currentWeapon = _newWeapon;

        if (previousWeapon != null)
        {
            previousWeapon.weaponEquipped = false;
        }

        _newWeapon.weaponEquipped = true;

        weaponPickupCallBack?.Invoke(_newWeapon);
        weaponPickupCallBack?.Invoke(previousWeapon);
        weaponPickupUICallBack?.Invoke();

        return true;
    }
}
