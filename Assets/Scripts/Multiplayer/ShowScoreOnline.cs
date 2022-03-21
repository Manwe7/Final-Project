using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer
{
    public class ShowScoreOnline : MonoBehaviourPunCallbacks
    {
        [Header("Texts")]
        [SerializeField] private Text[] _playerName;
        [SerializeField] private Text[] _playerScore;

        public const string LivesSaveKey = "RemainingLives";
        
        private void Awake()
        {
            Hashtable lives = new Hashtable
            {
                [LivesSaveKey] = 3
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(lives);
        }

        private void Start()
        {
            SetPlayerNames();
        }

        private void SetPlayerNames()
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                _playerName[i].text = PhotonNetwork.PlayerList[i].NickName;
            }
        }

        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
            
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey(LivesSaveKey))
                {
                    int lives = (int)PhotonNetwork.PlayerList[i].CustomProperties[LivesSaveKey];
                    _playerScore[i].text = $"{lives}";
                }
            }
        }
    }
}