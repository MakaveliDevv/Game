using UnityEngine;

public class WeaponInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private WeaponSlotUI slots;

    void Start() 
    {
        WeaponInventory.instance.weaponPickupUICallBack += UpdateWeaponUI;
        slots = itemsParent.GetComponentInChildren<WeaponSlotUI>();
    }

    void UpdateWeaponUI() 
    {
        if(WeaponInventory.instance.weapons.Count > 0) 
        {
            slots.AddWeaponUI(WeaponInventory.instance.weapons[0]);
        }
        else 
        {
            slots.ClearWeaponSLot();
        }
    }
}
