using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    #region Singleton
    public static WeaponInventory instance;

    void Awake() 
    {
        if(instance != null) 
        {
            Debug.Log("More than one instance of the weapon Inventory found!");
        }
        instance = this;
    }
    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();

    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;
    #endregion

    public int space = 1;
    public List<Weapon> weapons = new();

    public bool CanCollectWeapon() 
    {
        return weapons.Count <= space;
    }
    
      void Start()
    {
        // Find the default weapon and add it to the inventory if there is space
        Weapon defaultWeapon = FindDefaultWeapon();
        if (defaultWeapon != null && CanCollectWeapon())
        {
            weapons.Add(defaultWeapon);
            weaponPickupCallBack?.Invoke(defaultWeapon);
            weaponPickupUICallBack?.Invoke();
        }
    }

    public Weapon FindDefaultWeapon() 
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
        

        Weapon previousWeapon = weapons.Count > 0 ? weapons[0] : null;

        weapons.Clear();
        weapons.Add(_newWeapon);

        weaponPickupCallBack?.Invoke(_newWeapon);
        weaponPickupCallBack?.Invoke(previousWeapon);
        weaponPickupUICallBack?.Invoke();

        return true;
    }

    // public void RemoveWeapon(Weapon _weapon) 
    // {
    //     weapons.Remove(_weapon);
    //     weaponPickupCallBack?.Invoke(_weapon);
    //     // weaponPickupCallBackUI.Invoke();

    //     // Add the default weapon after removing to maintain the required space
    //     Weapon defaultWeapon = FindDefaultWeapon();
    //     if(defaultWeapon != null && CanCollectWeapon()) 
    //     {
    //         weapons.Add(defaultWeapon);
    //         weaponPickupCallBack?.Invoke(defaultWeapon);
    //     }
    // }
}
