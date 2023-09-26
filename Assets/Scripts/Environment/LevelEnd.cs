using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public AudioSource endJingle;

    public void OnTriggerEnter(Collider other)
    {
        StopAllAudio();
        StartCoroutine(FinalSequence());
    }

    IEnumerator FinalSequence()
    {
        endJingle.Play();
        yield return new WaitForSeconds(4.2f);
        SceneManager.LoadScene("FinalScene");
    }

    private AudioSource[] allAudioSources;

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

}
