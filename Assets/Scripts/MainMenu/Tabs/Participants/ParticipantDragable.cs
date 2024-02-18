using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a dragable object that bears the name of it's linked user
/// </summary>
public class ParticipantDragable : DragableObject
{
    [SerializeField] private Image bgImg;
    [SerializeField] private TextMeshProUGUI userNameText;
    private User user;
    private ParticipantsContainer container;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="user">The linked user</param>
    /// <param name="startingContainer">The starting container</param>
    public void Init(User user, ParticipantsContainer startingContainer)
    {
        this.user = user;
        container = startingContainer;
        userNameText.text = user.username;
    }

    /// <summary>
    /// OnBeginDrag event
    /// </summary>
    /// <param name="eventData">The event data</param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        bgImg.raycastTarget = false;
        userNameText.raycastTarget = false;
    }

    /// <summary>
    /// OnEndDrag event
    /// </summary>
    /// <param name="eventData">The event data</param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        bgImg.raycastTarget = true;
        userNameText.raycastTarget = true;
    }

    /// <summary>
    /// Changes the current container
    /// </summary>
    /// <param name="newContainer">The new container</param>
    public void SetContainer(ParticipantsContainer newContainer)
    {
        if (container == newContainer) return;

        container.RemoveUser(user);
        container = newContainer;
        container.AddUser(user);
    }
}
