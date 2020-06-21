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
    //remove empty methods. even emty methods are executed. will slgihtly decrease load
    private void Start()
    {
        //Play("MainSound");
    }
    
    //better to use Enum instead of string. wrong string == no sound
    //+ check if such sound exists in the collection
    public void Play(string name)
    {
        //Find the right audio and play it
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    
}
