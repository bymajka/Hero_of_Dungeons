using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource src;
    public float musicaudio = 0.2f;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }
    void Update()
    {
        src.volume = musicaudio;
    }

    public void SetVolume(float vol)
    {
        musicaudio = vol;
    }

    public float GetVolume()
    {
        return musicaudio;
    }
}
