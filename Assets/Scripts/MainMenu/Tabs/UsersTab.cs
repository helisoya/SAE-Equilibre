using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents the users tab, where you can view and add users
/// </summary>
public class UsersTab : MainMenuTab
{
    [Header("Users")]
    [SerializeField] private Transform usersRoot;
    [SerializeField] private GameObject userButtonPrefab;
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI userAgeText;
    [SerializeField] private Transform userSessionsRoot;
    [SerializeField] private GameObject userSessionPrefab;
    [SerializeField] private GameObject userInfoRoot;

    [Header("Add User")]
    [SerializeField] private GameObject addUserRoot;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private TMP_InputField inputFieldAge;

    private User currentUser;

    public override void Open()
    {
        base.Open();
        ClearChildUsers();
        ClearChildUserSessions();

        List<User> users = new List<User>(GameManager.instance.GetAllUsers());
        users.Sort(delegate (User u1, User u2)
        {
            return u1.username.CompareTo(u2.username);
        });

        foreach (User user in users)
        {
            Instantiate(userButtonPrefab, usersRoot).GetComponent<UserButton>().Init(user, this);
        }

        bool positiveNumberOfUsers = users.Count != 0;
        if (positiveNumberOfUsers)
        {
            Click_ChooseUser(users[0]);
        }
        userInfoRoot.SetActive(positiveNumberOfUsers);


        usersRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
        usersRoot.GetComponent<RectTransform>().sizeDelta.x,
        (userButtonPrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * users.Count);

    }

    /// <summary>
    /// Clears the childs of the users root
    /// </summary>
    void ClearChildUsers()
    {
        Utils.DestroyChildren(usersRoot);
    }

    /// <summary>
    /// Clears the childs of the user's sessions 
    /// </summary>
    void ClearChildUserSessions()
    {
        Utils.DestroyChildren(userSessionsRoot);
    }

    public override void Close()
    {
        base.Close();
        ClearChildUsers();
        ClearChildUserSessions();
        Click_CloseCreateUser();
    }

    /// <summary>
    /// Click event for opening the user creation window
    /// </summary>
    public void Click_OpenCreateUser()
    {
        addUserRoot.SetActive(true);
        inputFieldAge.SetTextWithoutNotify("");
        inputFieldName.SetTextWithoutNotify("");
    }

    /// <summary>
    /// Click event for closing the user creation window
    /// </summary>
    public void Click_CloseCreateUser()
    {
        addUserRoot.SetActive(false);
    }

    /// <summary>
    /// Click event for creating a new user
    /// </summary>
    public void Click_CreateUser()
    {
        string inputName = inputFieldName.text.Replace("\t", "");
        string inputAge = inputFieldAge.text.Replace("\t", "");
        if (string.IsNullOrEmpty(inputAge) || string.IsNullOrEmpty(inputName))
        {
            return;
        }

        if (!int.TryParse(inputAge, out int age))
        {
            return;
        }

        User user = new User(inputName, age);
        GameManager.instance.AddUser(user);

        Open();

        addUserRoot.SetActive(false);
    }

    /// <summary>
    /// Click event for choosing a new user
    /// </summary>
    /// <param name="user">The new user</param>
    public void Click_ChooseUser(User user)
    {
        currentUser = user;
        ClearChildUserSessions();

        userNameText.text = "Nom : " + user.username;
        userAgeText.text = "Age : " + user.age + " ans";

        Dictionary<string, UserMoveStats> movesForUser = new Dictionary<string, UserMoveStats>();

        foreach (Session session in user.sessions)
        {
            foreach (Session.SessionMovement movement in session.movements)
            {


                if (!movesForUser.ContainsKey(movement.moveID))
                {
                    movesForUser.Add(movement.moveID, new UserMoveStats(GameManager.instance.GetMovement(movement.moveID).movementName));
                }

                movesForUser[movement.moveID].total++;
                if (movement.wasCorrectlyDone)
                {
                    movesForUser[movement.moveID].success++;
                }
            }
        }

        foreach (UserMoveStats stat in movesForUser.Values)
        {
            Instantiate(userSessionPrefab, userSessionsRoot).GetComponent<UserSessionStat>().Init(stat);
        }


        userSessionsRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
        userSessionsRoot.GetComponent<RectTransform>().sizeDelta.x,
        (userSessionPrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * movesForUser.Values.Count
        );
    }

    /// <summary>
    /// Click event for returning to the title tab
    /// </summary>
    public void Click_ToTitle()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.titleTab);
    }

    /// <summary>
    /// Click event for deleting a user
    /// </summary>
    public void Click_DeleteUser()
    {
        if (currentUser == null) return;
        GameManager.instance.RemoveUser(currentUser);
        Open();
    }



}

/// <summary>
/// Represents the stats of a user, regarding a specific movement
/// </summary>
public class UserMoveStats
{
    public string moveName;
    public int total;
    public int success;

    public UserMoveStats(string name)
    {
        moveName = name;
    }
}
