using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private GamePlayButtons _gamePlayButtons = null;
        [SerializeField] private GamePlayUI _gamePlayUI = null;

        [Header("Record on pause panel")]
        [SerializeField] private Text _pauseRecordText = null;

        [Header("Buttons")] 
        [SerializeField] private Button _resumeButton = null;
        [SerializeField] private Button _restartButton = null;
        [SerializeField] private Button _exitButton = null;
        
        private int _record;

        private IGetRepo<SaveAttributes> _repoInt;

        SaveAttributes _saveAttributes;

        private void Awake()
        {                
            _repoInt = new SaveClassRepo();

            AddListenersToButtons();
        }
        
        private void AddListenersToButtons()
        {
            _restartButton.onClick.AddListener(_gamePlayButtons.Resume);
            _restartButton.onClick.AddListener(_gamePlayButtons.Restart);
            _exitButton.onClick.AddListener(_gamePlayButtons.Exit);
        }

        #region Event subscription
        
        private void OnEnable()
        {
            _gamePlayUI.OnGamePause += ShowRecord;
        }

        private void OnDestroy()
        {
            _gamePlayUI.OnGamePause -= ShowRecord;
        }
        
        #endregion

        private void ShowRecord()
        {
            _saveAttributes = _repoInt.Get();
            _record = _saveAttributes.Record;

            _pauseRecordText.text = _record.ToString();
        }

        
    }
}
