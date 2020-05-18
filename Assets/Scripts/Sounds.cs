using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip audioClip;
    
    [Range(0f, 1f)]
    public float volume, pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
    
    [Range (0f, 1f)]
    public float spacialBlend;
}
