using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
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