using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticipantsTab : MainMenuTab
{
    [Header("Participants Tab")]
    [SerializeField] private ParticipantsContainer notParticipating;
    [SerializeField] private ParticipantsContainer participating;
    [SerializeField] private GameObject prefabUser;
    [SerializeField] private ExercicesTab exercicesTab;

    public override void Open()
    {
        base.Open();

        List<User> users = GameManager.instance.GetAllUsers();
        foreach (User user in users)
        {
            Instantiate(prefabUser, notParticipating.root).GetComponent<ParticipantDragable>().Init(user, notParticipating);
            notParticipating.AddUser(user);
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
    }


    public void Click_Exercices()
    {
        Close();
        exercicesTab.Open();
    }

    public void Click_Start()
    {
        GameManager.instance.participants = participating.users;
        SceneManager.LoadScene("GameScene");
    }

}
