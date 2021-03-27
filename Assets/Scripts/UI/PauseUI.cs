using System;
using BaseClasses;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private GamePlayButtons _gamePlayButtons = null;
        
        [Header("Buttons")] 
        [SerializeField] private Button _resumeButton = null;
        [SerializeField] private Button _restartButton = null;
        [SerializeField] private Button _exitButton = null;
        
        private void Awake()
        {
            AddListenersToButtons();
        }
        
        private void AddListenersToButtons()
        {
            _resumeButton.onClick.AddListener(_gamePlayButtons.Resume);
            _restartButton.onClick.AddListener(_gamePlayButtons.Restart);
            _exitButton.onClick.AddListener(_gamePlayButtons.Exit);
        }
    }
}
