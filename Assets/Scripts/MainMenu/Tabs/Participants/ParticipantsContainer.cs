using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a container, where the user can drag users in and out
/// </summary>
public class ParticipantsContainer : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform userRoot;
    private List<User> _users;

    public Transform root
    {
        get { return userRoot; }
    }

    public List<User> users
    {
        get { return _users; }
    }


    void Awake()
    {
        _users = new List<User>();
    }

    /// <summary>
    /// OnDrop event
    /// </summary>
    /// <param name="eventData">The event data</param>
    public void OnDrop(PointerEventData eventData)
    {
        ParticipantDragable dropped = eventData.pointerDrag.GetComponent<ParticipantDragable>();

        if (dropped == null) return;

        dropped.SetParentAfterDrag(userRoot);
        dropped.SetContainer(this);

        userRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            userRoot.GetComponent<RectTransform>().sizeDelta.x,
            (dropped.GetComponent<RectTransform>().sizeDelta.y + 5) * (userRoot.childCount + 1)
        );
    }

    /// <summary>
    /// Adds a user to the container
    /// </summary>
    /// <param name="user">The user</param>
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    /// <summary>
    /// Removes a user from the container
    /// </summary>
    /// <param name="user"></param>
    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }

    /// <summary>
    /// Purge the container (deletes all the existing users)
    /// </summary>
    public void Purge()
    {
        _users.Clear();
        Utils.DestroyChildren(userRoot);
    }
}
