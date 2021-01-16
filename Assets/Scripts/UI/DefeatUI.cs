using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class DefeatUI : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private PlayerOfflineScipts.PlayerHealth _playerHealth;
        [SerializeField] private GameSpeed _gameSpeed;

        [Header("Defeat panel")]
        [SerializeField] private GameObject _defeatMenuPanel = null;

        [Header("Record on defeat panel")]
        [SerializeField] private Text _defeatRecordText;
        [SerializeField] private GameObject _adsPanel;

        private int _record;
        private bool _isAdsPanelOpened;
        private IRepo<SaveAttributes> _repoInt;
        private SaveAttributes _saveAttributes;

        private void Awake()
        {
            _gameSpeed.SetToNormal();

            _repoInt = new SaveClassRepo();
        
            _playerHealth.OnPlayerDefeated += Defeat;
        }    

        private void OnDisable()
        {
            _playerHealth.OnPlayerDefeated -= Defeat;
        }

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
            _saveAttributes = _repoInt.Get();
            _record = _saveAttributes.Record;

            _defeatRecordText.text = _record.ToString();
        
            if(_isAdsPanelOpened)
            {
                _defeatMenuPanel.SetActive(true);        
                _gameSpeed.Stop();            
            }
            else
            {
                _adsPanel.SetActive(true);
                _isAdsPanelOpened = true;
            }
        
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
            _gameSpeed.SetToNormal();
        }

        public void CloseAdsPanel()
        {
            OpenDefeatPanel();
        }

        public void Exit()
        {
            SceneManager.LoadScene(SceneNames.Menu);
        }    
    }
}
