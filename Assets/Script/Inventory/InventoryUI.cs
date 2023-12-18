using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private Inventoryy inventory;
    private InventorySlot[] slots;
    public GameObject inventoryUI;

    void Start()
    {
        inventory = Inventoryy.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory")) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI() 
    {
        for (int i = 0; i < slots.Length; i++)
        {   
            if(i < inventory.items.Count) 
            {
                slots[i].AddItem(inventory.items[i]);
            } else 
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("UPDATING UI");
    }
}
