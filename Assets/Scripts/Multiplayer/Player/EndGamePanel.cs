using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

namespace PlayerOnlineScripts
{
    public class EndGamePanel : MonoBehaviourPun
    {
        [SerializeField] private PlayerLivesOnlineSync _playerLivesOnlineSync;
        [SerializeField] private PhotonView _photonView;

        private GameObject _endPanel;
        private Text _endPanelText;

        private void Awake()
        {
            if (!_photonView.IsMine) { return; }

            FindUIObjects();
        }

        private void FindUIObjects()
        {
            _endPanel = GameObject.Find("Canvas/EndPanel");
            _endPanelText = GameObject.Find("Canvas/EndPanel/Text").GetComponent<Text>();
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
            for (var i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(PhotonNetwork.PlayerList[i].CustomProperties[ShowScoreOnline.LivesSaveKey] != null)
                {
                    var lives = (int)PhotonNetwork.PlayerList[i].CustomProperties[ShowScoreOnline.LivesSaveKey];
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
