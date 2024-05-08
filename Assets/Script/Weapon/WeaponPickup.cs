using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : Interactable, IDGameObject
{
    public Weapon weapon;
    [SerializeField] protected Transform weaponSlot;

    void Start() 
    {
        weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").transform;
    }

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
    
    public IEnumerator EquipTimer() 
    {
        yield return new WaitForSeconds(weapon.equipTimer);
        weapon.weaponEquipped = false;
        Destroy(gameObject);

        yield break;    
    }

    public GameObject Equip()
    {
        WeaponInventory.instance.weapons.Clear();
        WeaponInventory.instance.weapons.Add(weapon);
        WeaponInventory.instance.defWeaponObj.SetActive(false);

        GameObject weaponObj = Instantiate(weapon.wpnObject, weaponSlot.position, weaponSlot.rotation); 
        weaponObj.transform.SetParent(weaponSlot);
        
        Gun gunScript = weaponObj.AddComponent<Gun>();
        
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
    



