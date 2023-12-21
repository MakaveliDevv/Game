using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { RPG, ASSAULTRIFLE, SUBMACHINEGUN, RIFLE, SWORD }
public enum WeaponFireType { SINGLE, MULTIPLE, MELEE }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptables/Weapon")]
public class Weapon : ScriptableObject
{
    public static Weapon instance;

    void Awake()
    {
        instance = this;
    }

    new public string name = "Name";
    public WeaponType weaponType;
    public WeaponFireType fireType;
    public float bulletDamage;
    public float bulletVelocity;
    public float range;
    public float fireRate;
    private float nextTimeToFire;
    public float equipTimer;
    public float waitBeforeEquip;
    public bool isDefaultWeapon = false;
    public bool weaponEquipped = false;
    public Sprite icon = null; // Item icon
    public GameObject muzzleFlash = null;

    float NextTimeToFire() 
    {
        nextTimeToFire = Time.time + 1f/fireRate;
        return nextTimeToFire;
    }
}
