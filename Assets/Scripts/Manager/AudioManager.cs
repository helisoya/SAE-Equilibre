using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

/// <summary>
/// Class managing the audio of the game
/// </summary>
public class AudioManager
{
    private List<string> musicsAvailable;
    private Dictionary<string, AudioClip> musicClips;
    private string musicsPath;

    /// <summary>
    /// Initialize the AudioManager
    /// </summary>
    public AudioManager()
    {
        musicsPath = Application.streamingAssetsPath + "/Musics";
        musicClips = new Dictionary<string, AudioClip>();
        FindAvailableMusics();
    }

    /// <summary>
    /// Finds all available musics in the music folder
    /// </summary>
    void FindAvailableMusics()
    {
        string[] filesInDir = System.IO.Directory.GetFiles(musicsPath);
        musicsAvailable = new List<string>();

        foreach (string music in filesInDir)
        {
            if (music.EndsWith(".mp3"))
            {
                musicsAvailable.Add(System.IO.Path.GetFileName(music));
            }

        }
    }

    /// <summary>
    /// Returns the available musics
    /// </summary>
    /// <returns>The available musics</returns>
    public List<string> GetAvailableMusics()
    {
        return musicsAvailable;
    }

    /// <summary>
    /// Gets an Audio Clip, given it's name. It is loaded in memory if not already.
    /// </summary>
    /// <param name="clipName">The clip's name</param>
    /// <returns>The audio clip</returns>
    public async Task<AudioClip> GetClip(string clipName)
    {
        if (clipName == null) return null;

        if (musicClips.ContainsKey(clipName))
        {
            Debug.Log("Clip already loaded");
            return musicClips[clipName];
        }

        System.UriBuilder uriBuilder = new System.UriBuilder(musicsPath + "/" + clipName);


        Debug.Log("Searching for clip at " + uriBuilder.Uri);
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(uriBuilder.Uri, AudioType.MPEG))
        {
            uwr.SendWebRequest();

            while (!uwr.isDone)
            {
                await Task.Delay(5);
            }


            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError) Debug.Log($"{uwr.error}");
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                musicClips.Add(clipName, clip);
                return clip;
            }
        }
        return null;
    }
}
