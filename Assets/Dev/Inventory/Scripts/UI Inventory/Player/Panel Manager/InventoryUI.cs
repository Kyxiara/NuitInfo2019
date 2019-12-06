using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{    
    private InventorySlot[] slots;
    private Inventory inventory;

    private void Start()
    {
        
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = GetComponentsInChildren<InventorySlot>();
        //panel.SetActive(false) need to be call after instanciate all slots. in that case, slot can correctly execute their Awake method
        
        UpdateUI();
    }
    
    void UpdateUI()
    {
        //Debug.Log("Update the inventory");
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < inventory.itemsStack.Count)
            {
                slots[i].AddItem(inventory.itemsStack[i]);
            }
            else
            {
                slots[i].ClearSlot();
                
            }
        }
    }
}
