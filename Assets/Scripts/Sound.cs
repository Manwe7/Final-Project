using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum SoundNames
    {
        PlayerDeath,
        EnemyDeath,
        PlayerBullet,
        Hurt
    }
    public SoundNames SoundName;


    public AudioClip AudioClip;
    [Range(0f, 1f)] public float Volume;
    [Range(-3f, 3f)] public float Pitch;
    public bool Loop;
    [HideInInspector] public AudioSource Source;
    [Range (0f, 1f)] public float SpacialBlend;
}
