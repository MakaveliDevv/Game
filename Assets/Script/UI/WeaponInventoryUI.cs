using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private WeaponInventory inventory;
    private WeaponSlotUI[] slots;

    void Start() 
    {
        inventory = WeaponInventory.instance;
        inventory.weaponPickupCallBackUI += UpdateWeaponUI;

        slots = itemsParent.GetComponentsInChildren<WeaponSlotUI>();
    }

    void UpdateWeaponUI() 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.weapons.Count) 
            {
                slots[i].AddWeaponUI(inventory.weapons[i]);
            } else 
            {
                slots[i].ClearWeaponSLot();
            }
        }
        Debug.Log("UPDATING WEAPON UI");
    }
}
