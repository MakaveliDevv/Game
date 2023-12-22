using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class WeaponPickup : Interactable
{
    public Weapon weapon;
    private float equippedTimer;

    public override void Start()
    {
        base.Start();
        equippedTimer = weapon.equipTimer;
    }

    public override void Interact()
    {
        base.Interact();
        if(!weapon.isDefaultWeapon) 
        {
            Debug.Log("Interacting with a weapon");
            WeaponPickUp();
        }
    }
    
    void WeaponPickUp() 
    {
        bool pickedUpWeapon = WeaponInventory.instance.AddWeapon(weapon);
        if(pickedUpWeapon)
        {
            Debug.Log(transform.name + "picked up");
            StartCoroutine(EquipTimerTest());
        }
    }

    public IEnumerator EquipTimerTest() 
    {
        yield return new WaitForSeconds(weapon.equipTimer);
        Debug.Log("Timer ran out, destroying the weapon");
        weapon.weaponEquipped = false;
        Destroy(gameObject);

        yield break;    
    }
}