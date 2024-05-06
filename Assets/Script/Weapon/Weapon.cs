using UnityEngine;

public enum WeaponType { RPG, ASSAULTRIFLE, SUBMACHINEGUN, RIFLE, SWORD }
public enum WeaponFireType { SINGLE, MULTIPLE, MELEE }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptables/Weapon")]
public class Weapon : ScriptableObject
{ 
    // GameObjects & Components
    [Header("GameObjects & Components")]
    public GameObject wpnObject;
    public GameObject muzzleFlash = null;
    public GameObject bullet;
    public Sprite icon = null; // Item icon

    [Header("Types")]
    public WeaponType weaponType;
    public WeaponFireType fireType;

    [Header("Specs")]
    public string Name = "Name";
    public float bulletDamage = 10f;
    public float bulletVelocity;
    public float fireRate = 15f;
    public float range = 100f;
    public float nextTimeToFire;
    public float waitBeforeEquipTime;
    public float equipTimer;
    public float dropRate;
    public bool defaultWeapon = false;
    public bool weaponEquipped = false;
}
