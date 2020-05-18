using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager: MonoBehaviour
{
    public Sounds[] sounds;

    //Get all settings for audio clips
    private void Awake()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spacialBlend;
        }    
    }

    private void Start()
    {
        //Play("MainSound");
    }

    public void Play(string name)
    {
        //Find the right audio and play it
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    
}
