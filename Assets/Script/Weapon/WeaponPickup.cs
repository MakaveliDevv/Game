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
            Equip();
            Destroy(gameObject);
        }
    }

    public GameObject Equip()
    {
        WeaponInventory.instance.weapons.Clear();
        WeaponInventory.instance.AddWeapon(weapon);
        if(WeaponInventory.instance.defaultWeaponSlot.gameObject.activeInHierarchy)
        {
            WeaponInventory.instance.defaultWeaponSlot.gameObject.SetActive(false);
        }
        
        weaponObj = Instantiate(weapon.wpnObject, WeaponInventory.instance.weaponSlot.position, WeaponInventory.instance.weaponSlot.rotation);
        weaponObj.name = weapon.name; 
        weaponObj.transform.SetParent(WeaponInventory.instance.weaponSlot);

        WeaponInventory.instance.weaponGameObject = weaponObj;

        BoxCollider col = weaponObj.GetComponent<BoxCollider>();
        Destroy(col);
        
        // Destroy the weapon pickup component
        WeaponPickup pickup = weaponObj.GetComponent<WeaponPickup>();
        Destroy(pickup);

        // Check if weaponObj is a child of weaponSlot before adding the Gun component
        if (weaponObj.transform.parent == WeaponInventory.instance.weaponSlot)
        {
            weaponObj.AddComponent<Gun>();
        }
        else
        {
            Debug.LogWarning("weaponObj is not a child of the weaponSlot.");
        }    

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
    



