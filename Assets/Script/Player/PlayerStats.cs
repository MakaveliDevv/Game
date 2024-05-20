using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private readonly Dictionary<Stat, float> initialBaseValues = new();

    void Start()
    {
        PowerupInventory.instance.powerupPickupCallBack += OnPowerupPickUp;
    }

    void OnPowerupPickUp(Item _item)
    {
        // On powerup pickup, add the powerup modifiers
        if(_item != null) 
        {
            initialBaseValues[damage] = damage.ReturnBaseValue(); // Damage
            initialBaseValues[armor] = armor.ReturnBaseValue(); // Armor
            initialBaseValues[maxHealth] = maxHealth.ReturnBaseValue(); // Maxhealth
            initialBaseValues[walkSpeed] = walkSpeed.ReturnBaseValue(); // Movementspeed
            
            damage.AddModifier(_item.damageModifier); // Damage
            armor.AddModifier(_item.armorModifier); // Armor
            maxHealth.AddModifier(_item.maxHealthModifier); // Maxhealth
            walkSpeed.AddModifier(_item.movementModifier); // Movementspeed
            
            damage.SetValue(damage.GetValue()); // Damage
            armor.SetValue(armor.GetValue()); // Armor
            maxHealth.SetValue(maxHealth.GetValue()); // Maxhealth
            walkSpeed.SetValue(walkSpeed.GetValue()); // Movementspeed

            // Debug.Log("Mod-MaxHealth :" + maxHealth.GetValue());
            // Debug.Log("Init-MaxHealth :" + maxHealth.ReturnBaseValue());

            // Debug.Log("Mod-Armor :" + armor.GetValue());
            // Debug.Log("Init-Armor :" + armor.ReturnBaseValue());
            
        }

        StartCoroutine(ModifierCooldown(_item, _item.cooldownTimer));
    }
    
    private IEnumerator ModifierCooldown(Item _item, float _modifierCooldown) 
    {
        yield return new WaitForSeconds(_modifierCooldown);

        damage.RemoveModifier(_item.damageModifier); // Damage
        armor.RemoveModifier(_item.armorModifier); // Armor
        maxHealth.RemoveModifier(_item.maxHealthModifier); // Maxhealth
        walkSpeed.RemoveModifier(_item.movementModifier); // Maxhealth

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Modifiers removed from the list of modifiers");

        // Return the base value prior to picking up the modifier
        damage.SetValue(initialBaseValues[damage]); // Damage
        armor.SetValue(initialBaseValues[armor]); // Armor
        maxHealth.SetValue(initialBaseValues[maxHealth]); // Maxhealth
        walkSpeed.SetValue(initialBaseValues[walkSpeed]); // Movementspeed

        yield return new WaitForSeconds(.1f);

        Debug.Log("Base value returned");

        // Remove from the powerup list
        PowerupInventory.instance.RemoveItem(_item);
    }

    public override void Die()
    {
        base.Die();

        // Kill the player
        PlayerManager.instance.KillPlayer();
    }
}
