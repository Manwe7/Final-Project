using Managers;
using UnityEngine;

namespace Audio
{
    public class Melody : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _audioClips;

        [SerializeField] private AudioSource _audioSource;

        [Header("Scripts")] 
        [SerializeField] private DifficultyManager _difficultyManager;
    
        private void Start()
        {
            _audioSource.clip = _audioClips[_difficultyManager.DifficultyLevel];
        
            _audioSource.Play();
        }
    }
}
