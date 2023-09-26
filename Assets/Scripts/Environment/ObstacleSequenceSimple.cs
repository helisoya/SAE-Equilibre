using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ObstacleSequenceSimple : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject charModel;
    public string chosenAnimation;
    public VideoClip[] videoClips;
    public RawImage rawImage;

    [SerializeField]
    public VideoPlayer videoPlayer;

    public void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        charModel.GetComponent<Animator>().Play(chosenAnimation);
        Debug.Log("Le joueur a touchée un cubeTrigger et devrait jouer l'animation : " + chosenAnimation);
        rawImage.enabled = true;
        videoPlayer.clip = videoClips[0];
        videoPlayer.Play();
    }
}
