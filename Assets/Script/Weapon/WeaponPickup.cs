using System.Collections;
using UnityEngine;

public class WeaponPickup : Interactable
{
    public Weapon weapon;
    private WeaponInventory weaponInventory;


    public override void Interact()
    {
        base.Interact();
        if(!weapon.isDefaultWeapon) 
        {
            Debug.Log("Interacting with a weapon");
            WeaponPickUp();
        }
    }

    IEnumerator EquipAfterDelay()
    {
        yield return new WaitForSeconds(weapon.waitBeforeEquip);

        Debug.Log("Equipping Weapon...");

        yield break;
    }

    IEnumerator EquipTimer() 
    {
        yield return new WaitForSeconds(weapon.equipTimer);

        Debug.Log("Timer ran out, destroying the weapon");
        
        weapon.weaponEquipped = false;

        if(!weapon.isDefaultWeapon) 
        {
            // THIS IS SOMEHOW NOT WORKING AND I NEED TO CLEAR THE FUCKING LIST AHHHHH!!!!!!!!!!!!!!!!!!!!!!!
            weaponInventory.weapons.Clear(); // Access the singleton instance directly
            Destroy(gameObject);

        }


        yield break;
    }

    void WeaponPickUp() 
    {
        bool pickedUpWeapon = WeaponInventory.instance.AddWeapon(weapon);
        if(pickedUpWeapon)
        {
            Debug.Log(transform.name + "picked up");
            StartCoroutine( EquipAfterDelay() );
            Equip();
            
            // After the timer ran out, remove the weapon from the list

        }

        // return pickedUpWeapon;
    }

    float GetEquipDelayTimer() 
    {
        float currentTimer = weapon.waitBeforeEquip;
        return currentTimer;
    }

    float GetEquipTimer() 
    {
        float currentTimer = weapon.equipTimer;
        return currentTimer;
    }

    bool Equip() 
    {
        if(!weapon.isDefaultWeapon) 
        {
            weapon.weaponEquipped = true;
            StartCoroutine( EquipTimer() );

            weapon.waitBeforeEquip = GetEquipDelayTimer(); // Reset the timer  
            weapon.equipTimer = GetEquipTimer(); // Reset the timer
        }

        return false;
    }
}