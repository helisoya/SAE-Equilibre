using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the musics tab, where you can choose the music that will play during the exercice
/// </summary>
public class MusicsTab : MainMenuTab
{
    [Header("Musics Tab")]
    [SerializeField] private Transform musicsRoot;
    [SerializeField] private GameObject musicPrefab;

    public override void Open()
    {
        base.Open();
        DestroyExistingButtons();

        GameManager.instance.PlayVocalAssistantSFX(GameManager.instance.GetVocalAssistantData().musicListClip);

        Instantiate(musicPrefab, musicsRoot).GetComponent<MusicButton>().Init(null, this);

        List<string> musics = GameManager.instance.GetAllMusics();
        foreach (string music in musics)
        {
            Instantiate(musicPrefab, musicsRoot).GetComponent<MusicButton>().Init(music, this);
        }

        musicsRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            musicsRoot.GetComponent<RectTransform>().sizeDelta.x,
            (musicPrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * (musics.Count + 1)
        );
    }

    public override void Close()
    {
        base.Close();
        DestroyExistingButtons();
    }

    /// <summary>
    /// Destroys the music buttons
    /// </summary>
    void DestroyExistingButtons()
    {
        Utils.DestroyChildren(musicsRoot);
    }

    /// <summary>
    /// Click event for the button that opens the background tab
    /// </summary>
    /// <param name="music"></param>
    public void Click_ChooseMusic(string music)
    {
        GameManager.instance.currentMusic = music;
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.backgroundTab);
    }

    /// <summary>
    /// Click event for the button that opens the participants tab
    /// </summary>
    public void Click_ToParticipants()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.participantsTab);
    }
}
