using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicNameText;
    private MusicsTab tab;
    private string music;

    public void Init(string music, MusicsTab tab)
    {
        this.music = music;
        this.tab = tab;

        musicNameText.text = music.Replace(".mp3", "");
    }

    public void Click()
    {
        tab.Click_ChooseMusic(music);
    }
}
