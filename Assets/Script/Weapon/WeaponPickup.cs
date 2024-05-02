using System.Collections;
using UnityEngine;

public class WeaponPickup : Interactable
{
    public Weapon weapon;
    private GameObject instantiatedWeaponObject; // New field to store the instantiated weapon object


    public override void Start()
    {
        base.Start();
        // weaponHolder = WeaponManagement.instance.weaponHolder.gameObject;
    }

    public override void Interact()
    {
        base.Interact();
        if(!weapon.defaultWeapon) 
        {
            // Debug.Log("Interacting with a weapon");
            WeaponPickUp();
        }
    }
    
    void WeaponPickUp() 
    {
        bool pickedUpWeapon = WeaponInventory.instance.AddWeapon(weapon);
        if(pickedUpWeapon)
        {
            instantiatedWeaponObject = Equip();
            // Equip();
            // Debug.Log(transform.name + "picked up");
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

    public GameObject Equip()
    {
        StartCoroutine(AddWeaponWithDelay());
        // instantiatedWeaponObject = weapon.weaponGameObject;
        return instantiatedWeaponObject;
    }
}