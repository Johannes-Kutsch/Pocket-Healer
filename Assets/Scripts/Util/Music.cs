using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
    public static Music music;
    private AudioSource source;
    private int trackNumber;
    private AudioClip track;

    void Awake()
    {
        if (music == null)
        {
            DontDestroyOnLoad(gameObject);
            music = this;
        }
        else if (music != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        source = GameControl.control.source;
        trackNumber = UnityEngine.Random.Range(1, 9);
    }

    void Update()
    {
        if (!source.isPlaying)
        {
            switch (trackNumber) {
                case 1:
                    source.PlayOneShot(Resources.Load("Background_1", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 2:
                    source.PlayOneShot(Resources.Load("Background_2", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 3:
                    source.PlayOneShot(Resources.Load("Background_3", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 4:
                    source.PlayOneShot(Resources.Load("Background_4", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 5:
                    source.PlayOneShot(Resources.Load("Background_5", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 6:
                    source.PlayOneShot(Resources.Load("Background_6", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 7:
                    source.PlayOneShot(Resources.Load("Background_7", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 8:
                    source.PlayOneShot(Resources.Load("Background_8", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
                case 9:
                    source.PlayOneShot(Resources.Load("Background_9", typeof(AudioClip)) as AudioClip, 0.75f);
                    break;
            }
            if (trackNumber == 9)
                trackNumber = 0;
            else
                trackNumber++;
        }
    }
}
