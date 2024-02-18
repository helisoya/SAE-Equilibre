using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a button on the users tab.
/// Shows the corresponding user's informations when clicked
/// </summary>
public class UserButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicNameText;
    private UsersTab tab;
    private User user;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="user">The linked user</param>
    /// <param name="tab">The root tab</param>
    public void Init(User user, UsersTab tab)
    {
        this.user = user;
        this.tab = tab;

        musicNameText.text = user.username;
    }

    /// <summary>
    /// OnClick event
    /// </summary>
    public void Click()
    {
        tab.Click_ChooseUser(user);
    }
}
