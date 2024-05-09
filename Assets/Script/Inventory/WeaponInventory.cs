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

    public Weapon defaultWeapon, currentWeapon, previousWeapon;
    public GameObject weaponGameObject;
    public List<Weapon> weapons = new();
    [HideInInspector] public Transform weaponSlot, defaultWeaponSlot;
    public int space = 1;

    public delegate void OnWeaponPickup(Weapon _weapon);
    public delegate void OnWeaponPickupUI();
    public OnWeaponPickup weaponPickupCallBack;
    public OnWeaponPickupUI weaponPickupUICallBack;
    
    void Start()
    {     
        defaultWeaponSlot = GameObject.FindGameObjectWithTag("DefaultWeaponSlot").transform; // Slot
        weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").transform;
    
        GameObject defWeaponObj = Instantiate(defaultWeapon.wpnObject, defaultWeaponSlot.position, Quaternion.identity);
        defWeaponObj.transform.SetParent(defaultWeaponSlot);
        defWeaponObj.name = defaultWeapon.Name;

        if (defWeaponObj != null && CanCollectWeapon())
        {
            currentWeapon = defaultWeapon;
            weapons.Add(currentWeapon);

            Weapon weapon = weapons[0];
            if(weapon.defaultWeapon) 
            {
                weapon.equipped = true;
            }  
        }

        foreach (GameObject weapon in GameManager.instance.weapons)
        {
            if(weapon.TryGetComponent<WeaponPickup>(out var pickup)) 
            {
                pickup.weapon.equipped = false;
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


    public bool AddWeapon(Weapon _newWeapon) 
    {
        if (!CanCollectWeapon())
            return false;

        previousWeapon = currentWeapon;
        currentWeapon = _newWeapon;
        
        previousWeapon.equipped = false;
        
        if(weaponGameObject != null) 
            Destroy(weaponGameObject);

        _newWeapon.equipped = true;


        // weaponPickupCallBack?.Invoke(_newWeapon);
        // weaponPickupCallBack?.Invoke(previousWeapon);
        // weaponPickupUICallBack?.Invoke();

        return true;
    }
}
