using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    public AudioClip[] sound = new AudioClip[7];
    public AudioClip destroySound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    //SE再生メソッド(殴る音)
    public void TouchSound()
    {
        int n = Random.Range(1, sound.Length);
        audioSource.PlayOneShot(sound[n]);
    }

    //SE再生メソッド(爆発音)
    public void DestroySound() 
    {
        audioSource.PlayOneShot(destroySound);
    }
}