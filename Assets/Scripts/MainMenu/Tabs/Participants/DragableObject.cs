using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Represents a object that can be dragged
/// </summary>
public class DragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected Transform parentAfterDrag;

    /// <summary>
    /// OnBeginDrag event
    /// </summary>
    /// <param name="eventData">The event data</param>
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// OnDrag event
    /// </summary>
    /// <param name="eventData">The event data</param>
    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// OnEndDrag event
    /// </summary>
    /// <param name="eventData">The event data</param>
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// Changes the parent of the object after dragging
    /// </summary>
    /// <param name="newParent">The new parent after dragging</param>
    public void SetParentAfterDrag(Transform newParent)
    {
        parentAfterDrag = newParent;
    }
}
