using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{

    public ConsumableEffect consumableEffect;

    public int value;

    public override void Use()
    {
        base.Use();
        
        // use the item
        // remove it from the inventory
        Inventory.instance.Remove(this);
    }
}

public enum ConsumableEffect{ Health, Mana, Stamina}
