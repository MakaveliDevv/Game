using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private PowerupSlotUI[] slots;
    // public GameObject inventoryUI;

    void Start()
    {
        PowerupInventory.instance.powerupPickUpCallBackUI += UpdatePowerupUI;
        slots = itemsParent.GetComponentsInChildren<PowerupSlotUI>();
    }

    // void Update()
    // {
    //     if(Input.GetButtonDown("Inventory")) 
    //         inventoryUI.SetActive(!inventoryUI.activeSelf);
    // }

    void UpdatePowerupUI() 
    {
        for (int i = 0; i < slots.Length; i++)
        {   
            if(i < PowerupInventory.instance.powerups.Count) 
            {
                slots[i].AddPowerupUI(PowerupInventory.instance.powerups[i]);
            } else 
            {
                slots[i].ClearPowerupSlot();
            }
        }
        Debug.Log("UPDATING UI");
    }
}
