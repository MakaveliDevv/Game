using UnityEngine;

public enum WeaponType { RPG, ASSAULTRIFLE, SUBMACHINEGUN, RIFLE, SWORD }
public enum WeaponFireType { SINGLE, MULTIPLE, MELEE }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptables/Weapon")]
public class Weapon : ScriptableObject
{
    [HideInInspector] public GameObject weaponGameObject;
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
    public GameObject wpn = null;
}
