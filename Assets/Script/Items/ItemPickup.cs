using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    bool PickUp()
    {
        bool pickedUpBuff = Inventoryy.instance.Add(item);
        
        if(pickedUpBuff) 
            Destroy(gameObject);

        return pickedUpBuff;
    }
}