using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private PowerupInventory inventory;
    private PowerupSlotUI[] slots;
    // public GameObject inventoryUI;

    void Start()
    {
        inventory = PowerupInventory.instance;
        inventory.powerupPickUpCallBackUI += UpdatePowerupUI;

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
            if(i < inventory.powerups.Count) 
            {
                slots[i].AddPowerupUI(inventory.powerups[i]);
            } else 
            {
                slots[i].ClearPowerupSlot();
            }
        }
        Debug.Log("UPDATING UI");
    }
}
