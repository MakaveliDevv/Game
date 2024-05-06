// using System.Collections;
// using UnityEngine;

// public class WeaponPickup : Interactable
// {
//     public Weapon wpnSpecs;
//     private GameObject instantiatedWeaponObject; // New field to store the instantiated weapon object


//     // public override void Start()
//     // {
//         // base.Start();
//         // weaponHolder = WeaponManagement.instance.weaponHolder.gameObject;
//     // }

//     public override void Interact()
//     {
//         base.Interact();
//         if(!wpnSpecs.defaultWeapon) 
//         {
//             // Debug.Log("Interacting with a weapon");
//             WeaponPickUp();
//         }
//     }
    
//     void WeaponPickUp() 
//     {
//         bool pickedUpWeapon = WeaponInventory.instance.AddWeapon(wpnSpecs);
//         if(pickedUpWeapon)
//         {
//             instantiatedWeaponObject = Equip();
//             // Equip();
//             // Debug.Log(transform.name + "picked up");
//         }
//     }

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
    
//     public IEnumerator EquipTimer() 
//     {
//         yield return new WaitForSeconds(wpnSpecs.equipTimer);
//         // Debug.Log("Timer ran out, destroying the weapon");
//         wpnSpecs.weaponEquipped = false;
//         Destroy(gameObject);

//         yield break;    
//     }

//     public GameObject Equip()
//     {
//         StartCoroutine(AddWeaponWithDelay());
//         // instantiatedWeaponObject = weapon.weaponGameObject;
//         return instantiatedWeaponObject;
//     }
// }



using System.Collections;
using UnityEngine;

public class WeaponPickup : Interactable
{
    public Weapon weapon;

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

    public IEnumerator AddWeaponWithDelay()
    {
        yield return new WaitForSeconds(weapon.waitBeforeEquipTime);

        // Clear the list before adding a new one in the list
        WeaponInventory.instance.weapons.Clear();
        WeaponInventory.instance.weapons.Add(weapon);
        
        // StartCoroutine(EquipTimer());
        
        WeaponInventory.instance.weaponPickupCallBack?.Invoke(weapon);
        WeaponInventory.instance.weaponPickupUICallBack?.Invoke();

        yield break;
    }
    
    public IEnumerator EquipTimer() 
    {
        yield return new WaitForSeconds(weapon.equipTimer);
        // Debug.Log("Timer ran out, destroying the weapon");
        weapon.weaponEquipped = false;
        Destroy(gameObject);

        yield break;    
    }
    [SerializeField] private GameObject defaultWeapon;
    public GameObject Equip()
    {
        WeaponInventory.instance.weapons.Clear();
        WeaponInventory.instance.weapons.Add(weapon);

        // Instantiate the game object of that weapon
        GameObject weaponObj = Instantiate(weapon.wpnObject, weaponSlot.position, Quaternion.identity) as GameObject;
         
        // Place it in the weapon slot
        weaponObj.transform.SetParent(weaponSlot);

        // Get a reference to the default weapon thats equipped
        defaultWeapon = WeaponInventory.instance.defWeaponObj;
        
        // Set it inactive
        defaultWeapon.SetActive(false);

        return weaponObj;
    }
}