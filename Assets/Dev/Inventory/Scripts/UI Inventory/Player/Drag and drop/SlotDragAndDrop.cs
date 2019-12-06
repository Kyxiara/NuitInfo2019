using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SlotDragAndDrop : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    // drag options
    public PointerEventData.InputButton button = PointerEventData.InputButton.Left;
    public GameObject drageePrefab;
    GameObject currentlyDragged;

    // status
    public bool dragable = true;
    public bool dropable = true;
    

    public void OnBeginDrag(PointerEventData d)
    {
        // one mouse button is enough for dnd
        if (dragable && d.button == button)
        {
            // load current
            currentlyDragged = Instantiate(drageePrefab, transform.position, Quaternion.identity);
            
            //get the good slot depending where you start the drag (inventory/hotbar panel or equipment panel)
            InventorySlot inventorySlot = GetComponent<InventorySlot>();
//            EquipmentSlotScript equipmentSlotScript = GetComponent<EquipmentSlotScript>();

            ItemStack itemStack = null;
            
            if (inventorySlot != null)
            {
                itemStack = inventorySlot.ItemStack;
            }

            if (itemStack == null)
                return;

            currentlyDragged.GetComponent<InventorySlot>().AddItem(itemStack);
            currentlyDragged.transform.SetParent(transform.root, true); // canvas
            currentlyDragged.transform.SetAsLastSibling(); // move to foreground

            // disable button while dragging so onClick isn't fired if we drop a
            // slot on itself
            GetComponent<ClickableObject>().interactable = false;
        }
    }

    public void OnDrag(PointerEventData d)
    {
        // one mouse button is enough for drag and drop
        if (dragable && d.button == button)
            // move current
            currentlyDragged.transform.position = d.position;
    }

    // called after the slot's OnDrop
    public void OnEndDrag(PointerEventData d)
    {
        // delete current in any case
        Destroy(currentlyDragged);

        // one mouse button is enough for drag and drop
        if (dragable && d.button == button)
        {
            // try destroy if not dragged to a slot (flag will be set by slot)
            // message is sent to drag and drop handler for game specifics
            // -> only if dropping it into nirvana. do nothing if we just drop
            //    it on a panel. otherwise item slots are cleared if we
            //    accidentally drop it on the panel between two slots
            if (d.pointerEnter == null)
            {
                currentlyDragged.GetComponent<InventorySlot>().ClearSlot();
                Debug.Log("Drag & drop failed cause not on receiver");
            }

            // enable button again
            GetComponent<ClickableObject>().interactable = true;
        }
    }

    // d.pointerDrag is the object that was dragged
    public void OnDrop(PointerEventData d)
    {
        // one mouse button is enough for drag and drop
        if (dropable && d.button == button)
        {
            // was the dropped GameObject a UIDragAndDropable and was it dragable?
            // (Unity calls OnDrop even if .dragable was false)
            SlotDragAndDrop dropDragable = d.pointerDrag.GetComponent<SlotDragAndDrop>();
            if (dropDragable != null && dropDragable.dragable)
            {
                // only do something if we didn't drop it on itself. this way we
                // don't have to ignore raycasts etc.
                // message is sent to drag and drop handler for game specifics
                if (dropDragable != this)
                {
                    //Switch depending on tag of slot receiver
                    switch (gameObject.tag)
                    {
                        // if the slot is Equipment slot, try to equip if it's an equipment
                        case "EquipmentSlot":
                            DropOnEquipment(dropDragable);
                            break;
                        case "InventorySlot":
                            DropOnInventory(dropDragable);
                            break;
                        case "hotbarSlot":
                            DropOnHotbar(dropDragable);
                            break;
                        case "BinSlot":
                            DropOnBin(dropDragable);
                            break;
                    } 
                }
            }
        }
    }

    void OnDisable()
    {
        Destroy(currentlyDragged);
    }

    void OnDestroy()
    {
        Destroy(currentlyDragged);
    }

    private void DropOnEquipment(SlotDragAndDrop draggued)
    {
        // item 
        Item item = draggued.gameObject.GetComponent<InventorySlot>().ItemStack.item;
        if (item.GetType() == typeof(Equipment))
        {
            //Equip it
            item.Use();
        }
    }

    #region DropOnInventory

        private void DropOnInventory(SlotDragAndDrop draggued)
    {
        /* if it's an inventory slot,
         * - we drag an equipement: unequip equipment if this slot is not empty, reequip only if it's an equipment in this slot
         * - we drag inventory slot: switch both emplacement exept if this slot is empty.
         * - we drag hotbar slot: clear hotbarSlot if empty, else hotbar slot is link on this slot
         */
        switch (draggued.gameObject.tag)
        {
            case "EquipmentSlot":
//                DropEquipmentOnInventory(draggued);
                break;
            case "InventorySlot":
                SwitchInventoryItems(draggued);
                break;
            case "hotbarSlot":
                DropHotbarOnInventory(draggued);
                break;
        }
    }

//    private void DropEquipmentOnInventory(SlotDragAndDrop draggued)
//    {
//        EquipmentSlotScript equipmentSlot = draggued.gameObject.GetComponent<EquipmentSlotScript>();
//        ItemStack itemStack = GetComponent<InventorySlot>().ItemStack; 
//        
//        if (itemStack != null && itemStack.item.GetType() == typeof(Equipment))
//        {
//            // Equip the equipment of the received slot only if it's the same type equipmentSlot of the draggued object
//            // Correct the bug: drop a sword on helmet, then the sword isn't unequip and helmet is equip
//            Equipment equipment = (Equipment) itemStack.item;
//            if (equipment.equipSlot == equipmentSlot.equipmentSlot)
//            {
//                itemStack.item.Use();
//                return;
//            }
//        }
//        
//        equipmentSlot.Unequip();
//    }

    private void SwitchInventoryItems(SlotDragAndDrop draggued)
    {
        // if empty, move object to the end of the list else, switch item place
        ItemStack itemStackDraggued = draggued.GetComponent<InventorySlot>().ItemStack;
        //emptySlot
        if (!dragable)
        {
            Debug.Log("Is Empty slot");
            Inventory.instance.MoveItemToLastIndex(itemStackDraggued.item);
        }
        else
        {
            Debug.Log("Is not Empty slot");
            ItemStack itemStack = GetComponent<InventorySlot>().ItemStack;
            Inventory.instance.SwitchItemInInventory(itemStack.item, itemStackDraggued.item);
        }
    }

    private void DropHotbarOnInventory(SlotDragAndDrop draggued)
    {
        InventorySlot hotbarSlot = draggued.GetComponent<InventorySlot>();
        //emptySlot
        if (!dragable)
        {
            Hotbar.instance.Remove(hotbarSlot);
        }
        else
        {
            Hotbar.instance.AddItem(GetComponent<InventorySlot>().ItemStack, hotbarSlot);
        }
    }


    #endregion

    private void DropOnHotbar(SlotDragAndDrop draggued)
    {
        InventorySlot dragguedSlot = draggued.GetComponent<InventorySlot>();
        InventorySlot thisSlot = GetComponent<InventorySlot>();
        
        //hotbar to hotbar, just switch object position
        if (draggued.gameObject.CompareTag("hotbarSlot"))
        {
            Hotbar.instance.SwitchSlot(thisSlot, dragguedSlot);
        }
        else if (draggued.gameObject.CompareTag("InventorySlot"))
        {
            Hotbar.instance.BindItemInventoryWithSlot(dragguedSlot.ItemStack.item, thisSlot);
        }
    }
    
    private void DropOnBin(SlotDragAndDrop draggued)
    {
        //Only if object come from inventory directly
        if (draggued.gameObject.CompareTag("InventorySlot"))
        {
            ItemStack itemStackToDelete = draggued.gameObject.GetComponent<InventorySlot>().ItemStack;
            Inventory.instance.Remove(itemStackToDelete);
        }
    }

}
