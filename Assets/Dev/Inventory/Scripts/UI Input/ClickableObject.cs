using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

// Button script for 3 input mode, Left, Middle and Right Click
[RequireComponent(typeof(ButtonTransitioner))]
public class ClickableObject : Selectable, IPointerClickHandler {
    [FormerlySerializedAs("onLeftClick")]
    [SerializeField]
    private Button.ButtonClickedEvent m_OnLeftClick = new Button.ButtonClickedEvent();
    
    [FormerlySerializedAs("onRightClick")]
    [SerializeField]
    private Button.ButtonClickedEvent m_OnRightClick = new Button.ButtonClickedEvent();
    
    [FormerlySerializedAs("onMiddleClick")]
    [SerializeField]
    private Button.ButtonClickedEvent m_OnMiddleClick = new Button.ButtonClickedEvent();

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!IsActive() || !IsInteractable())
                return;
            m_OnLeftClick.Invoke();   
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            if (!IsActive() || !IsInteractable())
                return;
            m_OnMiddleClick.Invoke();   
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!IsActive() || !IsInteractable())
                return;
            m_OnRightClick.Invoke();   
        }
    }
    
    public Button.ButtonClickedEvent onLeftClick
    {
        get
        {
            return this.onLeftClick;
        }
        set
        {
            this.onLeftClick = value;
        }
    }
    
    public Button.ButtonClickedEvent onRightClick
    {
        get
        {
            return this.onRightClick;
        }
        set
        {
            this.onRightClick = value;
        }
    }
    
    public Button.ButtonClickedEvent onMiddleClick
    {
        get
        {
            return this.onMiddleClick;
        }
        set
        {
            this.onMiddleClick = value;
        }
    }

    public void testMiddle()
    {
        //Debug.Log("Middle clicked");
    }
    public void testRight()
    {
        //Debug.Log("Right clicked");
    }
    
    public void testLeft()
    {
        //Debug.Log("Left clicked");
    }
    
}