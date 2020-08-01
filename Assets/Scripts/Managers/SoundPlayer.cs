using UnityEngine;
using System;

public class SoundPlayer: MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (var s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();

            s.Source.clip = s.AudioClip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.spatialBlend = s.SpacialBlend;
        }    
    }
    
    public void Play(SoundNames soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == soundName);
        
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.Source.Play();
    }
    
}
