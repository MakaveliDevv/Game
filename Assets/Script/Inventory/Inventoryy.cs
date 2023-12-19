using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventoryy : MonoBehaviour
{
    #region Singleton
    public static Inventoryy instance;
    void Awake() 
    {
        if(instance != null) 
        {
            Debug.LogWarning("More than one isntance of Inventory found!");
        }
        instance = this;
    }
    #endregion 
    
    public delegate void OnItemPickup(Item item);
    public delegate void OnPickUp();
    public OnItemPickup itemPickupCallBack;
    public OnPickUp pickUpCallBack;
    
    public int space = 20;
    public List<Item> items = new();

    public bool CanCollectItem() 
    {
        return items.Count < space;
    }

    public bool Add(Item _item) 
    {
        if(!_item.isDefaultItem) 
        {
            if(!CanCollectItem()) 
                return false; 

            // continue to add item
            items.Add(_item);           
            itemPickupCallBack?.Invoke(_item);
            pickUpCallBack?.Invoke();
        }

        return true;        
    }

    public void Remove(Item _item) 
    {
        items.Remove(_item);
        itemPickupCallBack?.Invoke(_item);
        pickUpCallBack?.Invoke();
    }   
}
