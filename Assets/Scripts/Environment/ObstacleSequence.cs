using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

// On a besoin de définir une séqunce exacte de movement pour certaines sections, sinon utiliser ObstacleCollision
public class ObstacleSequence : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject charModel;
    public VideoClip[] videoClips;
    public RawImage rawImage;
    public int breakTime = 15; 
    // need to be able to specific break time through table but need some changes to be able to do that

    // easy way to get what a string is what in animation names
    public Dictionary<string, string> actions = new Dictionary<string, string>(){
    {"cueillirFleur", "Taking Item"},
    {"cueillirFleurMirror", "Taking Item Mirror"},
    {"equilibreRondin", "Falling From Losing Balance"},
    {"prendrePomme", "Unarmed Grab Torch From Wall"},
    {"retirerEnDessousPied", "Kick Soccerball"},
    {"stompHerbe", "Stomping"},
    {"break", "Breathing Idle"},
    {"ending", "Wave Hip Hop Dance"}
    };

    [SerializeField]
    public VideoPlayer videoPlayer;

    public string whatIsActionToTake;

    public void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(ExecuteSequence(whatIsActionToTake, charModel));

        Debug.Log(actions[whatIsActionToTake]);

        if (videoClips.Length > 0) {
            rawImage.enabled = true;
            videoPlayer.clip = videoClips[0];
            videoPlayer.Play();
        }

    }

    /* 
    private IEnumerator DoStuff(string sequence, GameObject charModel, int seconds)
    {
        thePlayer.GetComponent<PlayerMove>().enabled = false;
        charModel.GetComponent<Animator>().Play(actions[sequence]);
        yield return new WaitForSeconds(1);
        charModel.GetComponent<Animator>().Play("Standard Run");
        thePlayer.GetComponent<PlayerMove>().enabled = true;
    }
    */

    private void StartSequence(string sequence)
    {
        thePlayer.GetComponent<PlayerMove>().enabled = false;
        charModel.GetComponent<Animator>().Play(actions[sequence]);
    }

    private void EndSequence()
    {
        charModel.GetComponent<Animator>().Play("Standard Run");
        thePlayer.GetComponent<PlayerMove>().enabled = true;
    }

    public IEnumerator ExecuteSequence(string sequence, GameObject charModel)
    {
        switch (sequence)
        {
            case "cueillirFleur":
                StartSequence(sequence);
                yield return new WaitForSeconds(1);
                EndSequence();
                break;
            case "cueillirFleurMirror":
                StartSequence(sequence);
                yield return new WaitForSeconds(1);
                EndSequence();
                break;
            case "equilibreRondin": 
                charModel.transform.Translate(Vector3.down * 0.5f);
                StartSequence(sequence);
                yield return new WaitForSeconds(7);
                charModel.transform.Translate(Vector3.up * 0.5f);
                EndSequence();
                break;
            case "prendrePomme":
                StartSequence(sequence);
                yield return new WaitForSeconds(2);
                EndSequence();
                break;
            case "retirerEnDessousPied":
                StartSequence(sequence);
                yield return new WaitForSeconds(2);
                EndSequence();
                break;
            case "stompHerbe":
                StartSequence(sequence);
                yield return new WaitForSeconds(4);
                EndSequence();
                break;
            case "break":
                StartSequence(sequence);
                yield return new WaitForSeconds(10);
                EndSequence();
                break;
            case "ending":
                StartSequence(sequence);
                break;
            default:
                Debug.Log("ObstacleSequence manque une séquence à effectuer");
                break;

        }
    }
}
