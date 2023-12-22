using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    #region Singleton
    public static WeaponInventory instance;

    void Awake()
    {
        if(instance != null) 
        {
            Debug.Log("Message");
            Destroy(this);
            return;
        }

        // DontDestroyOnLoad(instance);
        instance = this;
    }
    #endregion

    private Weapon weaponInstance; // This is for the default weapon
    private Weapon currentWeapon; // Keep track of the currently equipped weapon
    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();

    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;

    public int space = 1;
    public List<Weapon> weapons = new();


    void Start()
    {
        // weaponInstance = Weapon.instance;

        // Set all weapons in the list except the first one as not default
        for (int i = 1; i < weapons.Count; i++)
        {
            weapons[i].isDefaultWeapon = false;
        }

        // Find the default weapon and add it to the inventory if there is space
        Weapon defaultWeapon = FindDefaultWeapon();
        if (defaultWeapon != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon; // Set the default weapon as the current weapon
            weapons.Add(currentWeapon); // Add current wpn to the list

            weaponInstance = defaultWeapon; // Assign the default weapon to weaponInstance
            weaponInstance.weaponEquipped = true;
            
            // Call back methods
            weaponPickupCallBack?.Invoke(defaultWeapon);
            weaponPickupUICallBack?.Invoke();

        }
    }

    public bool CanCollectWeapon()
    {
        return weapons.Count <= space;
    }

    public Weapon FindDefaultWeapon()
    {
        Weapon[] allWeapons = Resources.FindObjectsOfTypeAll<Weapon>();

        foreach (Weapon _weapon in allWeapons)
        {
            if (_weapon.isDefaultWeapon)
            {
                return _weapon;
            }
        }
        return null;
    }

    public bool AddWeapon(Weapon _newWeapon)
    {
        if (!CanCollectWeapon())
            return false;

        Weapon previousWeapon = currentWeapon;

        // Update the currently equipped weapon
        currentWeapon = _newWeapon;

        if (previousWeapon != null)
        {
            previousWeapon.weaponEquipped = false;
        }

        _newWeapon.weaponEquipped = true;

        weaponPickupCallBack?.Invoke(_newWeapon);
        weaponPickupCallBack?.Invoke(previousWeapon);
        weaponPickupUICallBack?.Invoke();

        StartCoroutine(AddWeaponWithDelay(_newWeapon));

        return true;
    }

    public IEnumerator AddWeaponWithDelay(Weapon newWeapon)
    {
        yield return new WaitForSeconds(newWeapon.waitBeforeEquip);

        // Clear the list before adding a new one in the list
        weapons.Clear();
        weapons.Add(newWeapon);

        weaponPickupCallBack?.Invoke(newWeapon);
        weaponPickupUICallBack?.Invoke();
        yield break;
    }

    public IEnumerator EquipTimer(Weapon currentWeapon)
    {
        yield return new WaitForSeconds(currentWeapon.equipTimer);

        Debug.Log("Timer ran out, destroying the weapon");
        currentWeapon.weaponEquipped = false;

        yield break;
    }
}
