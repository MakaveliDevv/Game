using UnityEngine;

public class WeaponPickup : Interactable, IDGameObject
{    
    public Weapon weapon;
    private GameObject weaponObj;

    public float GetDropRate() 
    {
        return weapon.dropRate;
    }

    public override void Interact()
    {
        base.Interact();
        if(!weapon.defaultWeapon) 
        {
            WeaponPickUp();
        }
    }
    
    void WeaponPickUp() 
    {
        bool pickedUpWeapon = WeaponInventory.instance.AddWeapon(weapon);
        if(pickedUpWeapon)
        {
            Destroy(gameObject);
            Equip();
        }
    }

    public GameObject Equip()
    {
        WeaponInventory.instance.weapons.Clear();
        WeaponInventory.instance.weapons.Add(weapon);
        WeaponInventory.instance.defaultWeaponSlot.gameObject.SetActive(false);
        
        weaponObj = Instantiate(weapon.wpnObject, WeaponInventory.instance.weaponSlot.position, WeaponInventory.instance.weaponSlot.rotation);
        weaponObj.name = weapon.name; 
        weaponObj.transform.SetParent(WeaponInventory.instance.weaponSlot);
        weaponObj.AddComponent<Gun>();

        WeaponInventory.instance.weaponGameObject = weaponObj;

        // Destroy the weapon pickup component
        WeaponPickup pickup = weaponObj.GetComponent<WeaponPickup>();
        Destroy(pickup);
        
        return weaponObj;
    }
}

// using System.Collections;

//     public IEnumerator AddWeaponWithDelay()
//     {
//         yield return new WaitForSeconds(wpnSpecs.waitBeforeEquipTime);

//         // Clear the list before adding a new one in the list
//         WeaponInventory.instance.weapons.Clear();
//         WeaponInventory.instance.weapons.Add(wpnSpecs);
        
//         // StartCoroutine(EquipTimer());
        
//         WeaponInventory.instance.weaponPickupCallBack?.Invoke(wpnSpecs);
//         WeaponInventory.instance.weaponPickupUICallBack?.Invoke();

//         yield break;
//     }
    



