﻿using UnityEngine;
using System.Collections;

/// <summary>
/// This class controlls the playback of music
/// </summary>
public class Music : MonoBehaviour {
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

    IEnumerator PlayNextBackroundTrack()
    {
        switch (trackNumber)
        {
            case 1:
                source.clip = Resources.Load("Background_1", typeof(AudioClip)) as AudioClip;
                break;
            case 2:
                source.clip = Resources.Load("Background_2", typeof(AudioClip)) as AudioClip;
                break;
            case 3:
                source.clip = Resources.Load("Background_3", typeof(AudioClip)) as AudioClip;
                break;
            case 4:
                source.clip = Resources.Load("Background_4", typeof(AudioClip)) as AudioClip;
                break;
            case 5:
                source.clip = Resources.Load("Background_5", typeof(AudioClip)) as AudioClip;
                break;
            case 6:
                source.clip = Resources.Load("Background_6", typeof(AudioClip)) as AudioClip;
                break;
            case 7:
                source.clip = Resources.Load("Background_7", typeof(AudioClip)) as AudioClip;
                break;
            case 8:
                source.clip = Resources.Load("Background_8", typeof(AudioClip)) as AudioClip;
                break;
            case 9:
                source.clip = Resources.Load("Background_9", typeof(AudioClip)) as AudioClip;
                break;
        }
        
        if (trackNumber == 9) //go to the next track
            trackNumber = 0;
        else
            trackNumber++;

        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        StartCoroutine("PlayNextBackroundTrack");
    }

    /// <summary>
    /// Called on every Update.
    /// If currently no backgroundtrack is played, start playing the new backgroundtrack.
    /// </summary>
    void FixedUpdate()
    {
        if (!source.isPlaying) //ToDo this is really sloppy and should be changed to a system that knows when an audioclip is finished.
            //ToDo option to disable music
        {
           /* switch (trackNumber) {
                case 1:
                    source.PlayOneShot(Resources.Load("Background_1", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 2:
                    source.PlayOneShot(Resources.Load("Background_2", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 3:
                    source.PlayOneShot(Resources.Load("Background_3", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 4:
                    source.PlayOneShot(Resources.Load("Background_4", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 5:
                    source.PlayOneShot(Resources.Load("Background_5", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 6:
                    source.PlayOneShot(Resources.Load("Background_6", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 7:
                    source.PlayOneShot(Resources.Load("Background_7", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 8:
                    source.PlayOneShot(Resources.Load("Background_8", typeof(AudioClip)) as AudioClip, 0.2f);
                    break;
                case 9:
                    source.PlayOneShot(Resources.Load("Background_9", typeof(AudioClip)) as AudioClip, 0.21f);
                    break;
            }*/
            if (trackNumber == 9) //go to the next track
                trackNumber = 0;
            else
                trackNumber++;
        }
    }
}
