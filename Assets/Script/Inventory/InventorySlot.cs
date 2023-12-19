using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Image icon; 
    private Item item;

    public void AddItem(Item _item) 
    {
        item = _item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot() 
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem() 
    {
        // Execute this method on pickup?
        if(item != null) 
            item.Use();
    }
}
