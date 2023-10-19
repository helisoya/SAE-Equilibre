using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void OnDrop(PointerEventData eventData)
    {
        ParticipantDragable dropped = eventData.pointerDrag.GetComponent<ParticipantDragable>();

        if (dropped == null) return;

        dropped.SetParentAfterDrap(userRoot);
        dropped.SetContainer(this);

        userRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            userRoot.GetComponent<RectTransform>().sizeDelta.x,
            (dropped.GetComponent<RectTransform>().sizeDelta.y + 5) * (userRoot.childCount + 1)
        );
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }

    public void Purge()
    {
        _users.Clear();
        foreach (Transform child in userRoot)
        {
            Destroy(child.gameObject);
        }
    }
}
