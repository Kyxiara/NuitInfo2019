using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(SlotDragAndDrop))]
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text stacks;
    protected ItemStack itemStack;
    private SlotDragAndDrop dragDrop;

    public ItemStack ItemStack => itemStack;
    
    protected virtual void Awake()
    {
        if (dragDrop == null)
        {
            dragDrop = gameObject.GetComponent<SlotDragAndDrop>();
            dragDrop.dragable = false;
        }
    }

    public virtual void AddItem(ItemStack newItem)
    {
        itemStack = newItem;
        
        
        //Debug.Log(itemStack.item.icon.name);
        icon.sprite = itemStack.item.icon;
        //Debug.Log(icon.sprite.name);

        icon.enabled = true;

        UpdateText();
        stacks.enabled = true;
        if (dragDrop == null)
        {
            dragDrop = gameObject.GetComponent<SlotDragAndDrop>();
        }
        dragDrop.dragable = true;
    }

    public virtual void ClearSlot()
    {
        itemStack = null;

        icon.sprite = null;
        icon.enabled = false;

        stacks.text = null;
        stacks.enabled = false;
        if (dragDrop == null)
        {
            dragDrop = gameObject.GetComponent<SlotDragAndDrop>();
        }
        dragDrop.dragable = true;
        dragDrop.dragable = false;
    }

    public virtual void RemoveItem()
    {
        if (itemStack != null)
        {
            Inventory.instance.Remove(itemStack.item);
            UpdateText();
        }
            
    }

    public void UseItem()
    {
        if (itemStack != null)
        {
            itemStack.item.Use();
        }
    }

    protected void UpdateText()
    {
        if (itemStack != null)
            stacks.text = itemStack.stacks.ToString();
    }
    
}
