using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private WeaponInventory inventory;
    private WeaponSlotUI slots;

    void Start() 
    {
        inventory = WeaponInventory.instance;
        inventory.weaponPickupUICallBack += UpdateWeaponUI;

        slots = itemsParent.GetComponentInChildren<WeaponSlotUI>();
    }

    void UpdateWeaponUI() 
    {
        if(inventory.weapons.Count > 0) 
        {
            slots.AddWeaponUI(inventory.weapons[0]);
        }
        else 
        {
            slots.ClearWeaponSLot();
        }
        Debug.Log("UPDATING WEAPON UI");
    }
}
