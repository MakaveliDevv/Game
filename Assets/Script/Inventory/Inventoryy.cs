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
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    
    public int space = 20;
    public List<Item> items = new();

    public bool CanCollectItem() 
    {
        return items.Count < space;
    }

    public bool Add(Item item) 
    {
        if(!item.isDefaultItem) 
        {
            if(!CanCollectItem()) 
            {
                Debug.Log("Not enough room");
                return false; 
            }

            // continue to add item
            items.Add(item);           
            onItemChangedCallBack?.Invoke();
        }

        return true;        
    }

    public void Remove(Item item) 
    {
        items.Remove(item);
        onItemChangedCallBack?.Invoke();
    }
   
}
