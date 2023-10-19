using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected Transform parentAfterDrag;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.SetAsLastSibling();
    }

    public void SetParentAfterDrap(Transform newParent)
    {
        parentAfterDrag = newParent;
    }
}
