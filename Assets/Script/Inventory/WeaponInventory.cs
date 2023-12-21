using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    #region Singleton
    public static WeaponInventory instance;

    void Awake() 
    {
        // if(instance != null) 
        // {
            // Debug.Log("More than one instance of the WeaponInventory found!");
            // Destroy(this.gameObject); // Destroy the duplicate instance
            // return;
            instance = this;
        // }
        // DontDestroyOnLoad(this.gameObject); // Ensure that this object persists between scenes
    }
    #endregion

    private Weapon weaponInstance;
    private Weapon currentWeapon; // Keep track of the currently equipped weapon
    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();

    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;

    public int space = 1;
    public List<Weapon> weapons = new();

    public bool CanCollectWeapon() 
    {
        return weapons.Count <= space;
    }
    
    void Start()
    {
        weaponInstance = Weapon.instance;

        // Set all weapons in the list except the first one as not default
        for (int i = 1; i < weapons.Count; i++)
        {
            weapons[i].isDefaultWeapon = false;
        }    

        // Find the default weapon and add it to the inventory if there is space
        Weapon defaultWeapon = FindDefaultWeapon();
        if (defaultWeapon != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon; // Set the default weapon as the current weapon
            weapons.Add(currentWeapon);

            weaponInstance = defaultWeapon; // Assign the default weapon to weaponInstance
            weaponInstance.weaponEquipped = true;

            // Call back methods
            weaponPickupCallBack?.Invoke(defaultWeapon);
            weaponPickupUICallBack?.Invoke();
        }
    }

    private Weapon FindDefaultWeapon() 
    {
        Weapon[] allWeapons = Resources.FindObjectsOfTypeAll<Weapon>();

        foreach (Weapon _weapon in allWeapons)
        {
            if(_weapon.isDefaultWeapon) 
            {
                return _weapon;
            }
        }
        return null;
    }

    public bool AddWeapon(Weapon _newWeapon) 
    {
        if(!CanCollectWeapon()) 
            return false;
        
        Weapon previousWeapon = currentWeapon;
        // Weapon previousWeapon = weapons.Count > 0 ? weapons[0] : null; // Get the previous weapon, if any

        weapons.Clear();
        weapons.Add(_newWeapon);

        // Update the currently equipped weapon
        currentWeapon = _newWeapon;

        if(previousWeapon != null) 
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
