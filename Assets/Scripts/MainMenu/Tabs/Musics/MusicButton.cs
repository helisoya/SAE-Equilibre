using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a button in the musics tab, that is used to select a music
/// </summary>
public class MusicButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicNameText;
    private MusicsTab tab;
    private string music;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="music">The music's name</param>
    /// <param name="tab">The root tab</param>
    public void Init(string music, MusicsTab tab)
    {
        this.music = music;
        this.tab = tab;

        musicNameText.text = music != null ? music.Replace(".mp3", "") : "Pas de musique";
    }

    /// <summary>
    /// OnClick event
    /// </summary>
    public void Click()
    {
        tab.Click_ChooseMusic(music);
    }
}
