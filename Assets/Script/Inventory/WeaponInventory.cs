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
    public OnWeaponPickupUI weaponPickupCallBackUI;
    #endregion

    public int space = 1;
    public List<Weapon> weapons = new();

    public bool AddWeapon(Weapon _weapon) 
    {
        if(!_weapon.isDefaultWeapon) 
        {
            weapons.Add(_weapon);
            
            // Invoke the method call back
            weaponPickupCallBack?.Invoke(_weapon);
            weaponPickupCallBackUI.Invoke();
        }
        return true;
    }

    public void RemoveWeapon(Weapon _weapon) 
    {
        weapons.Remove(_weapon);
        weaponPickupCallBack?.Invoke(_weapon);
        weaponPickupCallBackUI.Invoke();
    }
}
