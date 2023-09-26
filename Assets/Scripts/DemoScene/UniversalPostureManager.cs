using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UniversalPostureManager : MonoBehaviour
{

    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        int posture = LevelSelector.selectedPosture;

        if (posture == 1)
        {
            videoPlayer.clip = videoClips[0];
        }

        if (posture == 2)
        {
            videoPlayer.clip = videoClips[1];
        }

        if (posture == 3)
        {
            videoPlayer.clip = videoClips[2];
        }

        if (posture == 4)
        {
            videoPlayer.clip = videoClips[3];
        }

        if (posture == 5)
        {
            videoPlayer.clip = videoClips[4];
        }

        if (posture == 6)
        {
            videoPlayer.clip = videoClips[5];
        }

        if (posture == 7)
        {
            videoPlayer.clip = videoClips[6];
        }

        if (posture == 8)
        {
            videoPlayer.clip = videoClips[7];
        }

        if (posture == 9)
        {
            videoPlayer.clip = videoClips[8];
        }

        if (posture == 10)
        {
            videoPlayer.clip = videoClips[9];
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("DemoScene");
    }

}
