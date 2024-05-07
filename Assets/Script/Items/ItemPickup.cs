using UnityEngine;

public class ItemPickup : Interactable, IDGameObject
{
    public Item item;

    public float GetDropRate() 
    {
        return item.dropRate;
    }

    public override void Interact()
    {
        ItemPickUp();
    }

    bool ItemPickUp()
    {
        bool pickedUpItem = PowerupInventory.instance.AddItem(item);
        
        if(pickedUpItem) 
            Destroy(gameObject);

        return pickedUpItem;
    }
}