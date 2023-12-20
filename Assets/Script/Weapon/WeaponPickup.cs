using UnityEngine;

public class WeaponPickup : Interactable
{
    public Weapon weapon;

    public override void Interact()
    {
        base.Interact();
        if(!weapon.isDefaultWeapon) 
        {
            Debug.Log("Interacting with a weapon");
            WeaponPickUp();
        }
    }

    bool WeaponPickUp() 
    {
        bool pickedUpWeapon = WeaponInventory.instance.AddWeapon(weapon);
        if(pickedUpWeapon) 
            Destroy(gameObject);

        return pickedUpWeapon;
    }
}
