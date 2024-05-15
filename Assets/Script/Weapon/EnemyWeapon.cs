using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptables/EnemyWeapon")]
public class EnemyWeapon : ScriptableObject
{
    [Header("GameObjects & Components")]
    public GameObject wpnObject;
    public GameObject bullet;
    public GameObject muzzleFlash;

    [Header("Specs")]
    public string Name = "Name";
    public float bulletDamage = 25f;
    public float bulletVelocity;
    public float fireRate = 5f;
    public float range = 200f;
    [HideInInspector] public float nextTimeToFire;
}
