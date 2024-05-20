using UnityEngine;

public class WeaponInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private WeaponSlotUI slots;

    void Start() 
    {
        WeaponInventory.instance.weaponPickupCallBackUI += UpdateWeaponUI;
        slots = itemsParent.GetComponentInChildren<WeaponSlotUI>();

        WeaponInventory.instance.weaponRemoveCallBackUI += UpdateWeaponUI;
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
