using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp () 
    {        
        bool itemCollected = Inventoryy.instance.Add(item);
        if(itemCollected) 
            Destroy(gameObject);

        // bool objectCollected = _inventory.CollectWeapon(objectPrefab);

        // if(objectCollected) 
        // {
        //     Destroy(gameObject);
        //     Debug.Log("Item picked up, collecting " + item.name);
        // }
    }
}
