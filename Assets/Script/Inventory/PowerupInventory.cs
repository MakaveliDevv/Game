using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInventory : MonoBehaviour
{
    #region Singleton
    public static PowerupInventory instance;
    void Awake() 
    {
        if(instance != null) 
        {
            Destroy(this.gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); // Ensure that this object persists between scenes
    }
    #endregion 
    
    public delegate void OnPowerupPickup(Item item);
    public delegate void OnPowerupPickUpUI();
    public OnPowerupPickup powerupPickupCallBack;
    public OnPowerupPickUpUI powerupPickUpCallBackUI;

    public int space = 3;

    public List<Item> powerups = new();

    public bool CanCollectItem() 
    {
        return powerups.Count < space;
    }

    public bool AddItem(Item _item) 
    {
        if(!_item.isDefaultItem) 
        {
            if(!CanCollectItem()) 
                return false; 

            // continue to add item
            powerups.Add(_item);

            // Invoke the method call back	
            powerupPickupCallBack?.Invoke(_item);
            powerupPickUpCallBackUI?.Invoke();
        }

        return true;        
    }

    public void RemoveItem(Item _item) 
    {
        powerups.Remove(_item);
        powerupPickupCallBack?.Invoke(_item);
        powerupPickUpCallBackUI?.Invoke();
    }   
}
