using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicsTab : MainMenuTab
{
    [Header("Musics Tab")]
    [SerializeField] private Transform musicsRoot;
    [SerializeField] private GameObject musicPrefab;
    [SerializeField] private ParticipantsTab participantsTab;
    [SerializeField] private BackgroundTab backgroundTab;

    public override void Open()
    {
        base.Open();
        DestroyExistingButtons();

        List<string> musics = GameManager.instance.GetAllMusics();
        foreach (string music in musics)
        {
            Instantiate(musicPrefab, musicsRoot).GetComponent<MusicButton>().Init(music, this);
        }

        musicsRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            musicsRoot.GetComponent<RectTransform>().sizeDelta.x,
            (musicPrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * musics.Count
        );
    }

    public override void Close()
    {
        base.Close();
        DestroyExistingButtons();
    }

    void DestroyExistingButtons()
    {
        foreach (Transform child in musicsRoot)
        {
            Destroy(child.gameObject);
        }
    }


    public void Click_ChooseMusic(string music)
    {
        GameManager.instance.currentMusic = music;
        MainMenuManager.instance.StartTransition(this, backgroundTab);
    }

    public void Click_ToParticipants()
    {
        MainMenuManager.instance.StartTransition(this, participantsTab);
    }
}
