using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * add this script to button.
 * then on the original script of the button put transition and Navigation to None
 * make setting of visual interaction by this script
 */


public class ButtonTransitioner : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    
    public Color m_NormalColor = Color.white;
    public Color m_HoverColor = Color.grey;
    public Color m_DownColor = Color.white;

    public bool hoverColorDisabled = false;
    public bool downColorDisabled = false;

    private Image m_Image = null;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        if (m_Image != null)
        {
            m_NormalColor = m_Image.color;
            m_NormalColor.a = m_Image.color.a;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
//        Debug.Log("Enter");
        if(!hoverColorDisabled)
            m_Image.color = m_HoverColor;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
//        Debug.Log("Exit");

        m_Image.color = m_NormalColor;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
//        Debug.Log("Down");

        if(!downColorDisabled)
            m_Image.color = m_DownColor;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
//        Debug.Log("Up");

    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
//        Debug.Log("Click");

        if(!hoverColorDisabled)
            m_Image.color = m_HoverColor;
    }

    public void SetNormalColor()
    {
        m_Image.color = m_NormalColor;
    }
}
