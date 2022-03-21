using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class PlayerListEntry : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] public Text _playerNameText;
        [SerializeField] public Image _playerColorImage;
        [SerializeField] public Button _playerReadyButton;
        [SerializeField] public Image _playerReadyImage;

        private int _ownerId;
        private bool _isPlayerReady;

        private void OnEnable()
        {
            PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
        }
        
        private void OnDisable()
        {
            PlayerNumbering.OnPlayerNumberingChanged -= OnPlayerNumberingChanged;
        }

        public void Start()
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber != _ownerId)
            {
                _playerReadyButton.gameObject.SetActive(false);
            }
            else
            {
                Hashtable initialProps = new Hashtable { { LobbyConstants.PlayerIsReady, _isPlayerReady } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);
                PhotonNetwork.LocalPlayer.SetScore(0);

                _playerReadyButton.onClick.AddListener(() =>
                {
                    _isPlayerReady = !_isPlayerReady;
                    SetPlayerReady(_isPlayerReady);

                    Hashtable props = new Hashtable() {{LobbyConstants.PlayerIsReady, _isPlayerReady}};
                    PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        FindObjectOfType<Lobby>().LocalPlayerPropertiesUpdated();
                    }
                });
            }
        }
        
        public void Initialize(int playerId, string playerName)
        {
            _ownerId = playerId;
            _playerNameText.text = playerName;
        }

        private void OnPlayerNumberingChanged()
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.ActorNumber == _ownerId)
                {
                    _playerColorImage.color = AsteroidsGame.GetColor(player.GetPlayerNumber());
                }
            }
        }

        public void SetPlayerReady(bool playerReady)
        {
            _playerReadyButton.GetComponentInChildren<Text>().text = playerReady ? "Ready!" : "Ready?";
            _playerReadyImage.enabled = playerReady;
        }
    }
}
