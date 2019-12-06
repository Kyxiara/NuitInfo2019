using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SlotHolderManager : MonoBehaviour
{

    private Inventory _inventory;
    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform rectTransform;
    private float oldRaw;
    void Start()
    {
        oldRaw = 0;
        _inventory = Inventory.instance;
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        
        updateSize();
    }


    void updateSize()
    {
        float raw = Mathf.Ceil(_inventory.space / 4f);
        //Update only if we have a change
        if (oldRaw != raw)
        {
            oldRaw = raw;
            float rtHeight = (_gridLayoutGroup.spacing.y + _gridLayoutGroup.cellSize.y) * raw;

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rtHeight);
        }
    }
}
