using UnityEngine;
using System.Collections;

/// <summary>
/// This class controlls the playback of music
/// </summary>
public class Music : MonoBehaviour
{
    public static Music music;
    private AudioSource source;
    private int trackNumber;
    private AudioClip track;

    /// <summary>
    /// Called on Awake
    /// </summary>
    void Awake()
    {
        if (music == null)
        {
            DontDestroyOnLoad(gameObject); //Dont destroy this Object when transitioning to a new scene
            music = this;
        }
        else if (music != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called on start.
    /// Creates the audiosource and selects a random track.
    /// </summary>
    void Start()
    {
        source = GameControl.control.source;
        trackNumber = UnityEngine.Random.Range(1, 9);
        StartCoroutine("PlayNextBackroundTrack");
    }

    /// <summary>
    /// Plays a new Backgroundtrack and calls itself after the track has finished playing.
    /// </summary>
    IEnumerator PlayNextBackroundTrack()
    {
        source.clip = Resources.Load("Background_" + trackNumber, typeof(AudioClip)) as AudioClip;

        if (trackNumber == 9) //go to the next track
            trackNumber = 0;
        else
            trackNumber++;

        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        StartCoroutine("PlayNextBackroundTrack");
    }
}