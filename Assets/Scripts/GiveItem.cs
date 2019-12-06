using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
    public Item itemToGive;
    private bool alreadyGive = false;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.instance;
    }

    public bool give()
    {
        if (alreadyGive) return false;
        _inventory.Add(itemToGive);
        alreadyGive = true;
        return true;
    }

}
