using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { RPG, ASSAULTRIFLE, SUBMACHINEGUN, RIFLE, SWORD }
public enum WeaponFireType { SINGLE, MULTIPLE, MELEE }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptables/Weapon")]
public class Weapon : ScriptableObject
{
    new public string name = "Name";
    public WeaponType weaponType;
    public WeaponFireType fireType;
    public float bulletDamage;
    public float bulletVelocity;
    public float range;
    public float fireRate;
    private float nextTimeToFire;
    public Sprite icon = null; // Item icon
    public bool isDefaultWeapon = false;

    float NextTimeToFire() 
    {
        nextTimeToFire = Time.time + 1f/fireRate;
        return nextTimeToFire;
    }
}
