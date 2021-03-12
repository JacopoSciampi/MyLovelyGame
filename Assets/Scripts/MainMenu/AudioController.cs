using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Required")]
    public AudioSource myAudio;
    public string audioPath;
    public bool playOnAwake;

    [Header("Optionals")]
    public bool loopTrack;
    private void Awake()
    {
        if(playOnAwake)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        myAudio.clip = Resources.Load<AudioClip>(audioPath);
        myAudio.loop = loopTrack;

        myAudio.Play();
    }
}
