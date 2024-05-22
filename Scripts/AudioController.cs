using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioClip sound;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 2.5f)]
    public float pitch;

    public AudioSource source;

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();

        volume = 1f;
        pitch = 1f;

    }

    void Start()
    {
        source.clip = sound;
        source.volume = volume;
        source.pitch = pitch;

        // Joue automatiquement le son lors du démarrage de l'application
        PlayAndSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAndSound()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
        else
        {
            source.Pause();
        }
    }
}
