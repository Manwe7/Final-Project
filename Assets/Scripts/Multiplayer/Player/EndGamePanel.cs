using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using Multiplayer;
using Multiplayer.Player;

namespace PlayerOnlineScripts
{
    public class EndGamePanel : MonoBehaviourPun
    {
        [SerializeField] private PlayerLivesOnlineSync _playerLivesOnlineSync;
        [SerializeField] private PhotonView _photonView;

        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private GameObject _endPanel;
        private Text _endPanelText;

        private void Awake()
        {
            if (!_photonView.IsMine) { return; }

            _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();
            
            FindUIObjects();
        }

        private void FindUIObjects()
        {
            _endPanel = _playerOnlineProperties.EndPanel;
            _endPanelText = _playerOnlineProperties.EndPanelText;
        }

        private void Start()
        {
            if (!_photonView.IsMine) { return; }
            
            _endPanel.SetActive(false);
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            OpenEndGamePanel();
        }

        private void OpenEndGamePanel()
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(PhotonNetwork.PlayerList[i].CustomProperties[ShowScoreOnline.LivesSaveKey] != null)
                {
                    int lives = (int)PhotonNetwork.PlayerList[i].CustomProperties[ShowScoreOnline.LivesSaveKey];
                    if(lives <= 0)
                    {
                        StartCoroutine(StopGame());
                    }
                }
            }        
        }

        private IEnumerator StopGame()
        {
            yield return new WaitForSeconds(1.7f);
            Time.timeScale = 0.1f;
            _endPanel.SetActive(true);
            
            _endPanelText.text = _playerLivesOnlineSync.IsEnoughLives() ? "VICTORY" : "DEFEAT";
        }
    }
}
