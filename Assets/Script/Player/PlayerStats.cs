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
            // Store initial value
            initialBaseValues[damage] = damage.ReturnBaseValue(); // Damage
            initialBaseValues[armor] = armor.ReturnBaseValue(); // Armor
            initialBaseValues[maxHealth] = maxHealth.ReturnBaseValue(); // Maxhealth
            initialBaseValues[walkSpeed] = walkSpeed.ReturnBaseValue(); // Movementspeed
            
            // Add modifier
            damage.AddModifier(_item.damageModifier); // Damage
            armor.AddModifier(_item.armorModifier); // Armor
            maxHealth.AddModifier(_item.maxHealthModifier); // Maxhealth
            walkSpeed.AddModifier(_item.movementModifier); // Movementspeed
            
            // Set value
            damage.SetValue(damage.GetValue()); // Damage
            damageText.text = damage.GetValue().ToString();

            armor.SetValue(armor.GetValue()); // Armor
            armorText.text = armor.GetValue().ToString();

            maxHealth.SetValue(maxHealth.GetValue()); // MaxHealth
            maxHealthText.text = maxHealth.GetValue().ToString();


            walkSpeed.SetValue(walkSpeed.GetValue()); // Movementspeed
            moveSpeedText.text = walkSpeed.GetValue().ToString();
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
        damageText.text = damage.GetValue().ToString();

        armor.SetValue(initialBaseValues[armor]); // Armor
        armorText.text = armor.GetValue().ToString();

        maxHealth.SetValue(initialBaseValues[maxHealth]); // Maxhealth
        maxHealthText.text = maxHealth.GetValue().ToString();

        walkSpeed.SetValue(initialBaseValues[walkSpeed]); // Movementspeed
        moveSpeedText.text = walkSpeed.GetValue().ToString(); 

        Debug.Log("Base value returned");

        // Remove from the powerup list
        PowerupInventory.instance.RemoveItem(_item);
    }

    // private IEnumerator ModifierCooldown(Item _item, float _modifierCooldown) 
    // {
    //     float elapsedTime = 0f;

    //     while (elapsedTime < _modifierCooldown)
    //     {
    //         // Update the cooldown text with remaining time
    //         float remainingTime = _modifierCooldown - elapsedTime;
    //         maxHp_Mod.text = remainingTime.ToString("F2");

    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     } 

    //     damage.RemoveModifier(_item.damageModifier); // Damage
    //     armor.RemoveModifier(_item.armorModifier); // Armor
    //     maxHealth.RemoveModifier(_item.maxHealthModifier); // Maxhealth
    //     walkSpeed.RemoveModifier(_item.movementModifier); // Maxhealth

    //     yield return new WaitForSeconds(.15f);

    //     Debug.Log("Modifiers removed from the list of modifiers");

    //     // Return the base value prior to picking up the modifier
    //     damage.SetValue(initialBaseValues[damage]); // Damage
    //     damageText.text = damage.GetValue().ToString();

    //     armor.SetValue(initialBaseValues[armor]); // Armor
    //     armorText.text = armor.GetValue().ToString();

    //     maxHealth.SetValue(initialBaseValues[maxHealth]); // Maxhealth
    //     maxHealthText.text = maxHealth.GetValue().ToString();

    //     walkSpeed.SetValue(initialBaseValues[walkSpeed]); // Movementspeed
    //     moveSpeedText.text = walkSpeed.GetValue().ToString(); 

    //     yield return new WaitForSeconds(.1f);

    //     Debug.Log("Base value returned");

    //     // Remove from the powerup list
    //     PowerupInventory.instance.RemoveItem(_item);
    // }

    public override void Die()
    {
        base.Die();

        // Kill the player
        PlayerManager.instance.KillPlayer();
    }
}
