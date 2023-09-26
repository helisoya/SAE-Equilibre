using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class LevelStarter : MonoBehaviour
{

    [SerializeField]
    public VideoPlayer videoPlayer;

    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;
    public GameObject countDownGO;
    public GameObject fadeIn;
    public AudioSource countdownBip;
    public AudioSource countdownGo;
    public RawImage rawImage;

    void Start()
    {
        StartCoroutine(CountSequence());
    }

    IEnumerator CountSequence()
    {
        yield return new WaitForSeconds(1.5f);
        countDown3.SetActive(true);
        countdownBip.Play();
        yield return new WaitForSeconds(1);
        countDown2.SetActive(true);
        countdownBip.Play();
        yield return new WaitForSeconds(1);
        countDown1.SetActive(true);
        countdownBip.Play();
        yield return new WaitForSeconds(1);
        countDownGO.SetActive(true);
        countdownGo.Play();
        yield return new WaitForSeconds(1);
        rawImage.enabled = true;
        videoPlayer.Play();
        videoPlayer.loopPointReached += VideoEnd;
    }

    void VideoEnd(VideoPlayer vp)
    {
        rawImage.enabled = false;
    }

}
