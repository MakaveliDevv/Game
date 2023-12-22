using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    void Awake() 
    {
        instance = this;
    }

    public GameObject weapon;
    
}
