using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion 

    private Weapon currentWeapon; 

    public Weapon defaultWeapon;
    [HideInInspector] public GameObject defWeaponObj; 
    [HideInInspector] public Transform weaponSlot, defaultWeaponSlot;
    public List<Weapon> weapons = new();
    public int space = 1;

    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();
    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;
    
    void Start()
    {     
        defaultWeaponSlot = GameObject.FindGameObjectWithTag("DefaultWeaponSlot").transform; // Slot
        weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").transform;
    
        defWeaponObj = Instantiate(defaultWeapon.wpnObject, defaultWeaponSlot.position, Quaternion.identity) as GameObject;
        defWeaponObj.transform.SetParent(defaultWeaponSlot);
        defWeaponObj.name = defaultWeapon.Name;

        if (defWeaponObj != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon;
            weapons.Add(currentWeapon);

            // Loop through all the weapons
            foreach (var weapon in GameManager.instance.weapons)
            {
                WeaponPickup pickup = weapon.GetComponent<WeaponPickup>();
                
                if(!pickup.weapon.defaultWeapon)
                    pickup.weapon.weaponEquipped = false;
                else
                    pickup.weapon.weaponEquipped = false;
            }   
        }
    }

    public Weapon ReturnWeapon()
    {
        if(weapons.Count > 0) 
        {
            return weapons[0];

        } else
            return null;
    }

    public bool CanCollectWeapon()
    {
        return weapons.Count <= space;
    }


    public bool AddWeapon(Weapon _newWeapon) // Add to the scriptable object weapon list 
    {
        if (!CanCollectWeapon())
            return false;

        Weapon previousWeapon = currentWeapon;
        currentWeapon = _newWeapon;

        if (previousWeapon != null)
        {
            previousWeapon.weaponEquipped = false;
        }

        _newWeapon.weaponEquipped = true;

        weaponPickupCallBack?.Invoke(_newWeapon);
        weaponPickupCallBack?.Invoke(previousWeapon);
        weaponPickupUICallBack?.Invoke();

        return true;
    }
}
