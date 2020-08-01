using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOICEController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] gameStart;
    public AudioClip[] gameEnd;
    public AudioClip[] scoreCheck;
    public AudioClip[] count = new AudioClip[3];
    public AudioClip[] raceBeforeCount;
    public AudioClip[] raceStart;
    public AudioClip[] raceHappyEnd;
    public AudioClip[] raceBadEnd;
    public AudioClip[] retry;
    public AudioClip[] home;
    public AudioClip[] damage_fire;
    
 


    void Start()
    {
        
    }

    // //VOICE再生メソッド
    // public void VoiceSound(AudioClip[] sound)
    // {
    //     int n = Random.Range(0, sound.Length);
    //     audioSource.PlayOneShot(sound[n]);
    // }


    //VOICE再生メソッド(カウント時)
    public void CountVoiceSound(int num)
    {
        audioSource = GetComponent<AudioSource>();     
        if (num != 0)
        {
            audioSource.PlayOneShot(count[num-1]);
        }
        else
        {
            int n = Random.Range(0, this.raceStart.Length);
            audioSource.PlayOneShot(this.raceStart[n]);
        }
    }


    public void Play_gameStart()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameStart.Length > 0)
        {
            int n = Random.Range(0, gameStart.Length);
            AudioClip clip = gameStart[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_gameEnd()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameEnd.Length > 0)
        {
            int n = Random.Range(0, gameEnd.Length);
            AudioClip clip = gameEnd[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_scoreCheck()
    {
        audioSource = GetComponent<AudioSource>();

        if (scoreCheck.Length > 0)
        {
            int n = Random.Range(0, scoreCheck.Length);
            AudioClip clip = scoreCheck[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_raceBeforeCount()
    {
        audioSource = GetComponent<AudioSource>();

        if (raceBeforeCount.Length > 0)
        {
            int n = Random.Range(0, raceBeforeCount.Length);
            AudioClip clip = raceBeforeCount[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_raceStart()
    {
        audioSource = GetComponent<AudioSource>();

        if (raceStart.Length > 0)
        {
            int n = Random.Range(0, raceStart.Length);
            AudioClip clip = raceStart[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_raceHappyEnd()
    {
        audioSource = GetComponent<AudioSource>();

        if (raceHappyEnd.Length > 0)
        {
            int n = Random.Range(0, raceHappyEnd.Length);
            AudioClip clip = raceHappyEnd[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_raceBadEnd()
    {
        audioSource = GetComponent<AudioSource>();

        if (raceBadEnd.Length > 0)
        {
            int n = Random.Range(0, raceBadEnd.Length);
            AudioClip clip = raceBadEnd[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_retry()
    {
        audioSource = GetComponent<AudioSource>();

        if (retry.Length > 0)
        {
            int n = Random.Range(0, retry.Length);
            AudioClip clip = retry[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_home()
    {
        audioSource = GetComponent<AudioSource>();

        if (home.Length > 0)
        {
            int n = Random.Range(0, home.Length);
            AudioClip clip = home[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }

    public void Play_damage_fire()
    {
        audioSource = GetComponent<AudioSource>();

        if (damage_fire.Length > 0)
        {
            int n = Random.Range(0, damage_fire.Length);
            AudioClip clip = damage_fire[n];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClipが設定されていません");
        }
    }




}