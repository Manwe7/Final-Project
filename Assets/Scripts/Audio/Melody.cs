using System;
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
    
        private bool _isMelodyStarted;
        
        private void Start()
        {
            _audioSource.clip = _audioClips[_difficultyManager.DifficultyLevel];
        
            _audioSource.Play();
            _isMelodyStarted = true;
        }

        private void Update()
        {
            if (Time.timeScale == 0)
            {
                _audioSource.Pause();
                _isMelodyStarted = false;
            }
            else if(!_isMelodyStarted)
            {
                _audioSource.Play();
                _isMelodyStarted = true;
            }
        }
    }
}
