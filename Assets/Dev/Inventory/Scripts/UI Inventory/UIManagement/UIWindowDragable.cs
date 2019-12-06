// Adds window like behaviour to UI panels, so that they can be moved and closed
// by the user.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIWindowDragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // cache
    Transform window;

    void Awake()
    {
        // cache the parent window
        window = transform;
    }

    public void HandleDrag(PointerEventData d)
    {
        // move the parent
        window.Translate(d.delta);
    }

    public void OnBeginDrag(PointerEventData d)
    {
        HandleDrag(d);
    }

    public void OnDrag(PointerEventData d)
    {
        HandleDrag(d);
    }

    public void OnEndDrag(PointerEventData d)
    {
        HandleDrag(d);
    }
    
}
