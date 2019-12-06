using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public static Hotbar instance;
    
    public ItemStack[] items;

    private InventorySlot[] _inventorySlot;

    private void Awake()
    {
        instance = this;
        _inventorySlot = GetComponentsInChildren<InventorySlot>();
        items = new ItemStack[_inventorySlot.Length];
    }

    private void Start()
    {
        Inventory.instance.onItemChangedCallback += UpdateUI;
        Inventory.instance.onItemRemoveCallback += RemoveItem;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                _inventorySlot[i].AddItem(items[i]);
            }
            else _inventorySlot[i].ClearSlot();
        }
    }

    private void RemoveItem(ItemStack item)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == item)
            {
                items[i] = null;
            }
        }
    }

    public void Remove(InventorySlot inventorySlot)
    {
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i] == inventorySlot)
            {
                items[i] = null;
            }
        }
        UpdateUI();
    }

    public void AddItem(ItemStack itemStack, InventorySlot inventorySlot)
    {
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i] == inventorySlot)
            {
                items[i] = itemStack;
            }
        }
        UpdateUI();
    }

    public int GetIndexInventorySlot(InventorySlot inventorySlot)
    {
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i] == inventorySlot)
            {
                return i;
            }
        }
        return -1;
    }

    public void SwitchSlot(InventorySlot inventorySlot1, InventorySlot inventorySlot2)
    {
        int indexInventorySlot1 = GetIndexInventorySlot(inventorySlot1);
        int indexInventorySlot2 = GetIndexInventorySlot(inventorySlot2);

        ItemStack itemStacktmp = items[indexInventorySlot1];
        items[indexInventorySlot1] = items[indexInventorySlot2];
        items[indexInventorySlot2] = itemStacktmp;
        
        UpdateUI();
    }

    public void BindItemInventoryWithSlot(Item item, InventorySlot slot)
    {
        int indexItem = Inventory.instance.GetIndexItem(item);
        int indexSlot = GetIndexInventorySlot(slot);
        
        items[indexSlot] = Inventory.instance.itemsStack[indexItem];
        UpdateUI();
    }
}
