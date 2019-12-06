using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;


public class Inventory : MonoBehaviour
{
    #region Singleton

    public Item item;
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    #region Event

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public delegate void OnItemRemove(ItemStack itemStack);
    public OnItemRemove onItemRemoveCallback;
    
    public delegate void OnFailedConsume(Item item, int desiredStackConsume, int stackConsumed);
    public OnFailedConsume onFailedConsumeCallback;

    #endregion
    

    public List<ItemStack> itemsStack = new List<ItemStack>();

    public int space = 20;

    private void Start()
    {
        Debug.Log("did u have the item ?");
        Debug.Log(haveItem(item));
    }

    public bool Add(Item item)
    {
        /*
         * Add a single item in the inventory (ex: if you loot a single sword)
         */
        if (!item.isDefaultItem)
        {
            ItemStack itemStack = itemsStack.Find(x => x.item == item);
            if (itemStack != null)
            {
                itemStack.AddStack();
            }
            else
            {
                if (itemsStack.Count >= space)
                {
                    Debug.Log("Not enough space in inventory");
                    return false;
                }
                
                itemsStack.Add(new ItemStack(item));
            }

            onItemChangedCallback?.Invoke();

            return true;
        }
        return false;
    }

    public bool Add(Item item, int stack)
    {
        /*
         * Add an item in the inventory with is stack (ex: if you loot 3 same potion)
         */
        if (!item.isDefaultItem)
        {
            ItemStack itemStackInventory = itemsStack.Find(x => x.item == item);
            if (itemStackInventory != null)
            {
                itemStackInventory.AddStacks(stack);
            }
            else
            {
                if (itemsStack.Count >= space)
                {
                    Debug.Log("Not enough space in inventory");
                    return false;
                }
                //Debug.Log("ICI: " + item.name + stack.ToString() );
                itemsStack.Add(new ItemStack(item, stack));
            }

            onItemChangedCallback?.Invoke();

            return true;
        }
        return false;
    }

    public void Remove(Item item)
    {
        ItemStack itemStack = itemsStack.Find(x => x.item == item);
        if (itemStack != null)
        {
            itemStack.RemoveOneStack();
            if (itemStack.stacks == 0)
            {
                onItemRemoveCallback?.Invoke(itemStack);
                itemsStack.Remove(itemStack);
            }
            
            onItemChangedCallback?.Invoke();
        }
    }

    public void Remove(ItemStack itemStack)
    {
        itemsStack.Remove(itemStack);
        onItemRemoveCallback?.Invoke(itemStack);
        onItemChangedCallback?.Invoke();
    }

    public void RemoveStack(Item item, int stacks)
    {
        ItemStack itemStack = itemsStack.Find(x => x.item == item);
        if (itemStack != null)
        {
            int consumedStack = itemStack.RemoveMultipleStack(stacks);

            if (itemStack.stacks == 0)
            {
                
                onItemRemoveCallback?.Invoke(itemStack);
                itemsStack.Remove(itemStack);
            }
            
            if (consumedStack != stacks)
            {
                onFailedConsumeCallback?.Invoke(item, stacks, consumedStack);
            }

            onItemChangedCallback?.Invoke();
        }
    }

    public int GetIndexItem(Item item)
    {
        return itemsStack.FindIndex(x => x.item == item);
    }

    public bool SwitchItemInInventory(Item item1, Item item2)
    {
        int indexItem1 = GetIndexItem(item1);
        int indexItem2 = GetIndexItem(item2);

        ItemStack itemStackTmp = itemsStack[indexItem1];
        itemsStack[indexItem1] = itemsStack[indexItem2];
        itemsStack[indexItem2] = itemStackTmp;
        
        onItemChangedCallback?.Invoke();
        
        return true;
    }

    public void MoveItemToLastIndex(Item item)
    {
        int index = GetIndexItem(item);
        ItemStack itemStack = itemsStack[index];
        itemsStack.RemoveAt(index);
        
        itemsStack.Add(itemStack);
        onItemChangedCallback?.Invoke();
    }

    public bool haveItem(Item item)
    {
        ItemStack itemStack = itemsStack.Find(x => x.item == item);
        return itemStack != null;
    }
}
