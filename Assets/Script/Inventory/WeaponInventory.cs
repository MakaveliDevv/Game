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
            Destroy(this.gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); // Ensure that this object persists between scenes
    }
    #endregion 

    // public WeaponPickup defWeapon;
    private Weapon weaponInstance; // This is for the default weapon
    private Weapon currentWeapon; // Keep track of the currently equipped weapon
    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();

    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;

    public int space = 1;
    public List<Weapon> weapons = new();


    void Start()
    {
        // GameObject defaultWpnSlot = defWeapon.gameObject;
        GameObject defaultWpnSlot = GameObject.FindGameObjectWithTag("DefaultWpnSlot"); // Slot
        WeaponPickup defWeaponPickup = defaultWpnSlot.GetComponent<WeaponPickup>(); // Script
        
        GameObject defaultWeapon_obj = Instantiate(defWeaponPickup.weapon.wpnObject, defaultWpnSlot.transform.position, Quaternion.identity) as GameObject;
        defaultWeapon_obj.transform.SetParent(defaultWpnSlot.transform);
        defaultWeapon_obj.name = defWeaponPickup.weapon.name;

        // Find the default weapon and add it to the inventory if there is space
        Weapon defaultWeapon = FindDefaultWeapon();
        if (defaultWeapon_obj != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon; // Set the default weapon as the current weapon
            weapons.Add(currentWeapon); // Add current wpn to the list

            weaponInstance = currentWeapon; // Assign the default weapon to weaponInstance
            weaponInstance.weaponEquipped = true;
            
            // Call back methods
            weaponPickupCallBack?.Invoke(weaponInstance);
            weaponPickupUICallBack?.Invoke();

        }
    }

    public bool CanCollectWeapon()
    {
        return weapons.Count <= space;
    }

    
    public Weapon FindDefaultWeapons()
    {
        foreach (Weapon _weapon in weapons)
        {
            if (_weapon.defaultWeapon)
            {
                return _weapon;
            }
        }
        return null;
    }

    public Weapon FindDefaultWeapon()
    {
        GameObject defaultWeaponObject = GameObject.FindGameObjectWithTag("DefaultWeapon"); // Assuming you tagged the default weapon GameObject appropriately
        if (defaultWeaponObject != null)
        {
            defaultWeaponObject.TryGetComponent<WeaponPickup>(out var wpnScript);
            Weapon defaultWeapon = wpnScript.weapon;

            if(defaultWeapon != null && defaultWeapon.defaultWeapon)
                return defaultWeapon;
        }

        return null;
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
