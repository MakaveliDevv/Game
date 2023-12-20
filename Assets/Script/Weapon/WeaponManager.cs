using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // #region OldCode
    // public int selectedWeapon = 0;

    // void Start()
    // {
    //     SelectWeapon();
    // }

    // void Update()
    // {
    //     if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    //     {
    //         // Debug.Log("Mouse wheel going up");
    //         if (selectedWeapon >= transform.childCount - 1)
    //             selectedWeapon = 0;
    //         else
    //             selectedWeapon++;
    //     }

    //     if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    //     {
    //         // Debug.Log("Mouse wheel going down");

    //         if (selectedWeapon <= 0)
    //             selectedWeapon = transform.childCount - 1;
    //         else
    //             selectedWeapon--;
    //     }
    // }

    // void SelectWeapon()
    // {
    //     int i = 0;
    //     foreach (Transform weapon in transform)
    //     {
    //         weapon.gameObject.SetActive(false);

    //         if (i == selectedWeapon)
    //             weapon.gameObject.SetActive(true);
    //         i++;
    //     }
    // }
    // #endregion


    public static WeaponManager instance;

    void Awake() 
    {
        if(instance == null)
            instance = this;
        
        else { Destroy(gameObject); }
    }

    void Start() 
    {
        Weapon newDefaultWeapon = CreateDefaultWeapon();

        WeaponInventory.instance.AddWeapon(newDefaultWeapon);
    }

    Weapon CreateDefaultWeapon() 
    {
        Weapon defaultWeapon = ScriptableObject.CreateInstance<Weapon>();
        
        return defaultWeapon;
    }
}
