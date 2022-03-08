using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    AudioSource audioSource;
    public bool PlayOnStart;
    public bool loop;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
         audioSource.loop = loop;
        if (PlayOnStart)
            audioSource.Play();
    }

    public void Play()
    {
        audioSource.Play();
    }
}
