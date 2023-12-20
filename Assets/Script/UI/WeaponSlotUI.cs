using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSlotUI : MonoBehaviour
{
    public Image icon;
    private Weapon weapon;

    public void AddWeaponUI(Weapon _weapon) 
    {
        weapon = _weapon;
        icon.sprite = weapon.icon;
        icon.enabled = true;
    }

    public void ClearWeaponSLot() 
    {
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
