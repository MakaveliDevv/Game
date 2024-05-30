using UnityEngine.UI;
using UnityEngine;

public class PowerupSlotUI : MonoBehaviour
{
    public Image icon; 
    private Item item;

    public void AddPowerupUI(Item _item) 
    {
        item = _item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearPowerupSlot() 
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
