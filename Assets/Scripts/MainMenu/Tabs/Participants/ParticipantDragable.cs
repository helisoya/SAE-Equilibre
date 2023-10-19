using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ParticipantDragable : DragableObject
{
    [SerializeField] private Image bgImg;
    [SerializeField] private TextMeshProUGUI userNameText;
    private User user;
    private ParticipantsContainer container;

    public void Init(User user, ParticipantsContainer startingContainer)
    {
        this.user = user;
        container = startingContainer;
        userNameText.text = user.username;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        bgImg.raycastTarget = false;
        userNameText.raycastTarget = false;
    }


    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        bgImg.raycastTarget = true;
        userNameText.raycastTarget = true;
    }

    public void SetContainer(ParticipantsContainer newContainer)
    {
        if (container == newContainer) return;

        container.RemoveUser(user);
        container = newContainer;
        container.AddUser(user);
    }
}
