using System.Collections;
using BaseClasses;
using BasePlayer;
using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefeatUI : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private GamePlayButtons _gamePlayButtons = null;
        [SerializeField] private PlayerHealth _playerHealth = null;
        [SerializeField] private GameSpeed _gameSpeed = null;
        [SerializeField] private AdsManager _adsManager = null;

        [Header("Defeat panel")]
        [SerializeField] private GameObject _defeatMenuPanel = null;
        [SerializeField] private GameObject _adsPanel = null;

        [Header("Record on defeat panel")] 
        [SerializeField] private Text _recordText;

        [Header("Buttons")]
        [SerializeField] private Button _restartButton = null;
        [SerializeField] private Button _exitButton = null;

        private bool _isAdsPanelOpened;

        private void Awake()
        {
            AddListenersToButtons();
        }

        private void AddListenersToButtons()
        {
            _restartButton.onClick.AddListener(_gamePlayButtons.Restart);
            _exitButton.onClick.AddListener(_gamePlayButtons.Exit);
        }

        #region Event subscription
        
        private void OnEnable()
        {
            _adsManager.OnAdWatched += TurnAdsPanelOff;
            _playerHealth.OnPlayerDefeated += Defeat;
        }

        private void OnDisable()
        {
            _adsManager.OnAdWatched -= TurnAdsPanelOff;
            _playerHealth.OnPlayerDefeated -= Defeat;
        }
        
        #endregion

        private void Defeat()
        {        
            _gameSpeed.SetToHalfSpeed();        
            StartCoroutine(StartOpeningDefeatPanel());
        }

        private IEnumerator StartOpeningDefeatPanel()
        {
            yield return new WaitForSeconds(1.5f);
            OpenDefeatPanel();
        }
    
        private void OpenDefeatPanel()
        {
            if(_isAdsPanelOpened)
            {
                _defeatMenuPanel.SetActive(true);        
                _gameSpeed.StopTime();            
            }
            else
            {
                _adsPanel.SetActive(true);
                _isAdsPanelOpened = true;
            }
        
        }

        private void TurnAdsPanelOff()
        {
            _gameSpeed.ResumeTime();
            _adsPanel.SetActive(false);
        }

        public void CloseAdsPanel()
        {
            OpenDefeatPanel();
        }

        public void ShowRecord(int record)
        {
            print("show");
            _recordText.text = $"{record}";
        }
    }
}
