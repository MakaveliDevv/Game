using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Inventoryy inv;

    void Start()
    {
        Inventoryy.instance.itemPickupCallBack += OnItemPickUp;
    }

    void OnItemPickUp(Item item)
    {
        if(item != null) 
        {
            damage.AddModifier(item.damageModifier);
            armor.AddModifier(item.armorModifier);
        }
    }

    public override void Die()
    {
        base.Die();

        // Kill the player
        PlayerManager.instance.KillPlayer();
    }
}
