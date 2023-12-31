using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticipantsTab : MainMenuTab
{
    [Header("Participants Tab")]
    [SerializeField] private ParticipantsContainer notParticipating;
    [SerializeField] private ParticipantsContainer participating;
    [SerializeField] private GameObject prefabUser;

    public override void Open()
    {
        base.Open();

        List<User> users = GameManager.instance.GetAllUsers();
        ParticipantsContainer selectedContainer;
        foreach (User user in users)
        {
            selectedContainer = GameManager.instance.participants.Contains(user) ? participating : notParticipating;
            Instantiate(prefabUser, selectedContainer.root).GetComponent<ParticipantDragable>().Init(user, selectedContainer);
            selectedContainer.AddUser(user);
        }
    }


    void PurgeUsers()
    {
        notParticipating.Purge();
        participating.Purge();
    }

    public override void Close()
    {
        base.Close();
        PurgeUsers();
        GameManager.instance.SaveUsers();
    }


    public void Click_Exercices()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.exercicesTab);
    }

    public void Click_Start()
    {
        GameManager.instance.participants = new List<User>(participating.users);
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.musicsTab);

    }

}
