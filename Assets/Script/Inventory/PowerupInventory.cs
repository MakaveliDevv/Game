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
            Destroy(gameObject); // Destroy the duplicate instance
            return;
        }
        instance = this;
        // DontDestroyOnLoad(gameObject); // Ensure that this object persists between scenes
    }
    #endregion 
    
    public delegate void OnPowerupPickup(Item item);
    public OnPowerupPickup powerupPickupCallBack;

    public delegate void OnPowerupPickUpUI();
    public OnPowerupPickUpUI powerupPickUpCallBackUI;

    public delegate void OnPowerupRemove(Item item);
    public OnPowerupRemove powerupRemoveCallBack;
    
    public delegate void OnPowerupRemoveUI();
    public OnPowerupRemoveUI powerupRemoveCallBackUI;

    public int space = 4;

    public List<Item> powerups = new();

    public bool CanCollectItem() 
    {
        return powerups.Count < space;
    }

    public bool AddItem(Item _item) 
    {
        if(!_item.isDefaultItem && CanCollectItem() && !powerups.Contains(_item)) 
        {               
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
        powerupRemoveCallBack?.Invoke(_item);
        powerupRemoveCallBackUI?.Invoke();
    }   
}
