public class PlayerStats : CharacterStats
{
    void Start()
    {
        PowerupInventory.instance.powerupPickupCallBack += OnPowerupPickUp;
    }

    void OnPowerupPickUp(Item item)
    {
        // On powerup pickup, add the powerup modifiers
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
