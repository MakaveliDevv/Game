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
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion 

    public Weapon defaultWeapon, currentWeapon, previousWeapon;
    public GameObject weaponGameObject;
    public List<Weapon> weapons = new();
    [HideInInspector] public Transform weaponSlot, defaultWeaponSlot;
    public int space = 1;

    public delegate void OnWeaponPickup(Weapon weapon);
    public OnWeaponPickup weaponPickupCallBack;

    public delegate void OnWeaponPickupUI();
    public OnWeaponPickupUI weaponPickupCallBackUI;

    public delegate void OnWeaponRemove(Weapon weapon);
    public OnWeaponRemove weaponRemoveCallBack;

    public delegate void OnWeaponRemoveUI();
    public OnWeaponRemoveUI weaponRemoveCallBackUI;
    
    void Start()
    {     
        defaultWeaponSlot = GameObject.FindGameObjectWithTag("DefaultWeaponSlot").transform; // Slot
        weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").transform;
    
        GameObject defWeaponObj = Instantiate(defaultWeapon.wpnObject, defaultWeaponSlot.position, defaultWeaponSlot.rotation);
        defWeaponObj.transform.SetParent(defaultWeaponSlot);
        defWeaponObj.name = defaultWeapon.Name;

        if (defWeaponObj != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon;
            AddWeapon(currentWeapon);
            // weapons.Add(currentWeapon);

            Weapon weapon = weapons[0];
            if(weapon.defaultWeapon) 
            {
                weapon.equipped = true;
            }  
        }

        foreach (GameObject weapon in GameManager.instance.weapons)
        {
            if(weapon.TryGetComponent<WeaponPickup>(out var pickup)) 
            {
                pickup.weapon.equipped = false;
            }
        }
    }

    public Weapon ReturnWeapon()
    {
        if(weapons.Count > 0) 
        {
            return weapons[0];

        } else
            return null;
    }

    public bool CanCollectWeapon()
    {
        return weapons.Count <= space;
    }


    // public bool AddWeapon(Weapon _newWeapon) 
    // {
    //     // Check if we can collect a weapon
    //     if (!CanCollectWeapon())
    //         return false;

    //     // Store the current weapon as previous if it exists
    //     // if (currentWeapon != null)
    //     // {
    //     Weapon tempWpn = currentWeapon;

    //     currentWeapon = _newWeapon;
    //     currentWeapon.equipped = true;
    //     tempWpn.equipped = false;

    //         // previousWeapon = currentWeapon;
    //         // previousWeapon.equipped = false;
    //     // }

    //     // Destroy the previous weapon game object if it exists
    //     if (weaponGameObject != null) 
    //         Destroy(weaponGameObject);

    //     // Initialize the new weapon as the current one
    //     // currentWeapon = _newWeapon; // Switch the weapon to the picked-up one
    //     // currentWeapon.equipped = true;

    //     // Add the weapon to the weapons list if not already added
    //     if (!weapons.Contains(currentWeapon))
    //     {
    //         weapons.Add(currentWeapon);
    //         previousWeapon = tempWpn;
    //     }

    //     // Callbacks
    //     weaponPickupCallBack?.Invoke(currentWeapon);
    //     weaponPickupCallBack?.Invoke(previousWeapon);
    //     weaponPickupCallBackUI?.Invoke();

    //     return true;
    // }

    public bool AddWeapon(Weapon _newWeapon) 
    {
        // Check if we can collect a wpn
        if (!CanCollectWeapon())
            return false;
    
        
        // Store the current wpn
        previousWeapon = currentWeapon; // The very first time it's the default wpn
        
        // Unequip the previous one
        previousWeapon.equipped = false;
        
        // The weaponGameobject is the weaponObj that's been passed in the Equip method
        if(weaponGameObject != null) 
            Destroy(weaponGameObject);

        // Initialize the new weapon as the current one
        currentWeapon = _newWeapon; // Switch the wpn to the picked up one

        // Equip the new wpn
        currentWeapon.equipped = true;

        // Add the wpn to the weapons list
        weapons.Add(currentWeapon);

        // Callbacks
        weaponPickupCallBack?.Invoke(currentWeapon);
        weaponPickupCallBack?.Invoke(previousWeapon);
        weaponPickupCallBackUI?.Invoke();

        return true;
    }

    public void RemoveWeapon(Weapon _weapon)
    {
        weapons.Remove(_weapon);
        weaponRemoveCallBack?.Invoke(_weapon);
        weaponRemoveCallBackUI?.Invoke();
    } 
}
