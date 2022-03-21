using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

namespace Lobby
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        [Header("Scripts")] 
        [SerializeField] private GameSpeed _gameSpeed;
        
        [Header("Login Panel")] 
        [SerializeField] private Button _loginButton;
        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private InputField _nickNameInputField;
    
        [Header("Selection Panel")]
        [SerializeField] private GameObject _selectionPanel;
        [SerializeField] private Button _createRoomOptionButton;
        [SerializeField] private Button _specificRoomOptionButton;
        [SerializeField] private Button _joinRandomRoomOptionButton;
        [SerializeField] private Button _roomListOptionButton;

        [Header("Create room panel")]
        [SerializeField] private GameObject _createRoomPanel;
        [SerializeField] private InputField _roomNameInputField;
        [SerializeField] private InputField _maxPlayersInputField;
        [SerializeField] private Button _createRoomButton;

        [Header("Join Random Room Panel")]
        [SerializeField] private GameObject _joinRandomRoomPanel;
    
        [Header("Inside Room Panel")]
        [SerializeField] private GameObject _insideRoomPanel;
        [SerializeField] private GameObject _playerListContent;
        [SerializeField] private GameObject _playerListEntryPrefab;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _leaveGameButton;
    
        [Header("Room List Panel")]
        [SerializeField] private GameObject _roomListPanel;
        [SerializeField] private GameObject _roomListContent;
        [SerializeField] private GameObject _roomListEntryPrefab;

        [Header("Specific Room")] 
        [SerializeField] private GameObject _specificRoomPanel;
        [SerializeField] private InputField _specificRoomNameInputField;
        [SerializeField] private Button _joinSpecificRoomButton;

        [Header("Loading Text")] 
        [SerializeField] private GameObject _loadingPanel;

        [Header("Error panels")] 
        [SerializeField] private GameObject _nickNameErrorPanel;
        [SerializeField] private GameObject _roomNameErrorPanel;
        [SerializeField] private GameObject _noRoomErrorPanel;

        [Header("Back buttons")] 
        [SerializeField] private Button[] _backButtons;
        [SerializeField] private Button _backToMenu;
    
        private Dictionary<string, RoomInfo> _cachedRoomList;
        private Dictionary<string, GameObject> _roomListEntries;
        private Dictionary<int, GameObject> _playerListEntries;
        
        // Used in PlayerListEntry
        public void LocalPlayerPropertiesUpdated()
        {
            _startGameButton.gameObject.SetActive(CheckPlayersReady());
        }
    
        private void Awake()
        {
            _gameSpeed.ResumeTime();
        
            PhotonNetwork.AutomaticallySyncScene = true;

            _cachedRoomList = new Dictionary<string, RoomInfo>();
            _roomListEntries = new Dictionary<string, GameObject>();

            AssignButtons();
            
            if (PhotonNetwork.IsConnected)
            {
                SetActivePanel(_selectionPanel.name);
            }
        }

        private void AssignButtons()
        {
            _backToMenu.onClick.AddListener(OpenMenu);
            
            _loginButton.onClick.AddListener(Login);
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinSpecificRoomButton.onClick.AddListener(JoinSpecificRoom);
            _startGameButton.onClick.AddListener(StartGame);
            _leaveGameButton.onClick.AddListener(LeaveGame);

            _createRoomOptionButton.onClick.AddListener(()=> SetActivePanel("CreateRoomPanel"));
            _specificRoomOptionButton.onClick.AddListener(OpenSpecificRoomPanel);
            _joinRandomRoomOptionButton.onClick.AddListener(OpenJoinRandomRoomPanel);
            _roomListOptionButton.onClick.AddListener(OpenRoomListPanel);

            foreach (Button button in _backButtons)
            {
                button.onClick.AddListener(OpenLoginPanel);
            }
        }

        private IEnumerator ShowErrorPanel(GameObject panel)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(1.7f);
            panel.SetActive(false);
        }
    
        #region PUN CALLBACKS
        public override void OnConnectedToMaster()
        {
            SetActivePanel(_selectionPanel.name);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            ClearRoomListView();

            UpdateCachedRoomList(roomList);
            UpdateRoomListView();
        }

        public override void OnLeftLobby()
        {
            _cachedRoomList.Clear();

            ClearRoomListView();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            SetActivePanel(_selectionPanel.name);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            StartCoroutine(ShowErrorPanel(_noRoomErrorPanel));
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            string roomName = "Room " + Random.Range(1000, 10000);

            var options = new RoomOptions {MaxPlayers = 2};

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        public override void OnJoinedRoom()
        {
            SetActivePanel(_insideRoomPanel.name);

            if (_playerListEntries == null)
                _playerListEntries = new Dictionary<int, GameObject>();
            
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                GameObject entry = Instantiate(_playerListEntryPrefab);
                entry.transform.SetParent(_playerListContent.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<PlayerListEntry>().Initialize(player.ActorNumber, player.NickName);

                if (player.CustomProperties.TryGetValue(LobbyConstants.PlayerIsReady, out object isPlayerReady))
                {
                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);
                }

                _playerListEntries.Add(player.ActorNumber, entry);
            }

            _startGameButton.gameObject.SetActive(CheckPlayersReady());

            Hashtable props = new Hashtable
            {
                { LobbyConstants.PlayerLoadedLevel, false }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnLeftRoom()
        {
            SetActivePanel(_selectionPanel.name);

            foreach (GameObject entry in _playerListEntries.Values)
            {
                Destroy(entry.gameObject);
            }

            _playerListEntries.Clear();
            _playerListEntries = null;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GameObject entry = Instantiate(_playerListEntryPrefab);
            entry.transform.SetParent(_playerListContent.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<PlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

            _playerListEntries.Add(newPlayer.ActorNumber, entry);

            _startGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Destroy(_playerListEntries[otherPlayer.ActorNumber].gameObject);
            _playerListEntries.Remove(otherPlayer.ActorNumber);

            _startGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
            {
                _startGameButton.gameObject.SetActive(CheckPlayersReady());
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (_playerListEntries == null)
            {
                _playerListEntries = new Dictionary<int, GameObject>();
            }

            GameObject entry;
            if (_playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
            {
                object isPlayerReady;
                if (changedProps.TryGetValue(LobbyConstants.PlayerIsReady, out isPlayerReady))
                {
                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);
                }
            }

            _startGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        #endregion

        #region Buttons

        private void OpenMenu()
        {
            SceneManager.LoadScene(SceneNames.Menu);
        }
        
        private void OpenLoginPanel()
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }

            SetActivePanel(_selectionPanel.name);
        }
        
        private void OpenJoinRandomRoomPanel()
        {
            SetActivePanel(_joinRandomRoomPanel.name);

            PhotonNetwork.JoinRandomRoom();
        }
        
        private void OpenSpecificRoomPanel()
        {
            SetActivePanel(_specificRoomPanel.name);
        
            if (ProcessDeepLinkMngr.RoomName == null)
            {
                _specificRoomNameInputField.text = "";
                return;
            }
            _specificRoomNameInputField.text = ProcessDeepLinkMngr.RoomName;
        }

        private void OpenRoomListPanel()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }

            SetActivePanel(_roomListPanel.name);
        }
        
        private void JoinSpecificRoom()
        {
            PhotonNetwork.JoinRoom(_specificRoomNameInputField.text);
        }

        private void CreateRoom()
        {
            string roomName = _roomNameInputField.text;

            if (roomName.Equals(string.Empty))
            {
                StartCoroutine(ShowErrorPanel(_roomNameErrorPanel));
                return;
            }
            
            roomName = (roomName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;

            byte.TryParse(_maxPlayersInputField.text, out byte maxPlayers);
            maxPlayers = (byte) Mathf.Clamp(maxPlayers, 2, 2);

            RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        private void Login()
        {
            string playerName = _nickNameInputField.text;

            if (!playerName.Equals(string.Empty))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();

                _loadingPanel.SetActive(true);
            }
            else
            {
                StartCoroutine(ShowErrorPanel(_nickNameErrorPanel));
            }
        }
        
        private void StartGame()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            PhotonNetwork.LoadLevel("GameOnline");
        }
        
        private void LeaveGame()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        private bool CheckPlayersReady()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return false;
            }
        
            foreach (var p in PhotonNetwork.PlayerList)
            {
                object isPlayerReady;
                if (p.CustomProperties.TryGetValue(LobbyConstants.PlayerIsReady, out isPlayerReady))
                {
                    if (!(bool) isPlayerReady)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        
            return true;
        }

        private void ClearRoomListView()
        {
            foreach (var entry in _roomListEntries.Values)
            {
                Destroy(entry.gameObject);
            }

            _roomListEntries.Clear();
        }

        private void SetActivePanel(string activePanel)
        {
            _loginPanel.SetActive(activePanel.Equals(_loginPanel.name));
            _selectionPanel.SetActive(activePanel.Equals(_selectionPanel.name));
            _createRoomPanel.SetActive(activePanel.Equals(_createRoomPanel.name));
            _joinRandomRoomPanel.SetActive(activePanel.Equals(_joinRandomRoomPanel.name));
            _roomListPanel.SetActive(activePanel.Equals(_roomListPanel.name));    // UI should call OnRoomListButtonClicked() to activate this
            _insideRoomPanel.SetActive(activePanel.Equals(_insideRoomPanel.name));
            _specificRoomPanel.SetActive(activePanel.Equals(_specificRoomPanel.name));
        }
    
        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            foreach (var info in roomList)
            {
                // Remove room from cached room list if it got closed, became invisible or was marked as removed
                if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
                {
                    if (_cachedRoomList.ContainsKey(info.Name))
                    {
                        _cachedRoomList.Remove(info.Name);
                    }

                    continue;
                }

                // Update cached room info
                if (_cachedRoomList.ContainsKey(info.Name))
                {
                    _cachedRoomList[info.Name] = info;
                }
                // Add new room info to cache
                else
                {
                    _cachedRoomList.Add(info.Name, info);
                }
            }
        }
    
        private void UpdateRoomListView()
        {
            foreach (var info in _cachedRoomList.Values)
            {
                var entry = Instantiate(_roomListEntryPrefab);
                entry.transform.SetParent(_roomListContent.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<RoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, info.MaxPlayers);

                _roomListEntries.Add(info.Name, entry);
            }
        }
    }
}
