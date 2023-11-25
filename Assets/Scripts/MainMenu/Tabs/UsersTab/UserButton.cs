using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicNameText;
    private UsersTab tab;
    private User user;

    public void Init(User user, UsersTab tab)
    {
        this.user = user;
        this.tab = tab;

        musicNameText.text = user.username;
    }

    public void Click()
    {
        tab.Click_ChooseUser(user);
    }
}
