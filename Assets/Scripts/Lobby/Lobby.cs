using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    [Header("Login Panel")] 
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private InputField _nickNameInputField;
    
    [Header("Selection Panel")]
    [SerializeField] private GameObject _selectionPanel;
    
    [Header("Create room panel")]
    [SerializeField] private GameObject _createRoomPanel;
    [SerializeField] private InputField _roomNameInputField;
    [SerializeField] private InputField _maxPlayersInputField;
    
    [Header("Join Random Room Panel")]
    [SerializeField] private GameObject _joinRandomRoomPanel;
    
    [Header("Inside Room Panel")]
    [SerializeField] private GameObject _insideRoomPanel;
    [SerializeField] private GameObject _playerListContent;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameObject _playerListEntryPrefab;
    
    [Header("Room List Panel")]
    [SerializeField] private GameObject _roomListPanel;
    [SerializeField] private GameObject _roomListContent;
    [SerializeField] private GameObject _roomListEntryPrefab;

    [Header("Specific Room")] 
    [SerializeField] private GameObject _specificRoomPanel;
    [SerializeField] private InputField _specificRoomNameInputField;

    [Header("Game Speed")] 
    [SerializeField] private GameSpeed _gameSpeed;
    
    private Dictionary<string, RoomInfo> _cachedRoomList;
    private Dictionary<string, GameObject> _roomListEntries;
    private Dictionary<int, GameObject> _playerListEntries;
    
    public void Awake()
    {
        _gameSpeed.SetToNormal();
        
        PhotonNetwork.AutomaticallySyncScene = true;

        _cachedRoomList = new Dictionary<string, RoomInfo>();
        _roomListEntries = new Dictionary<string, GameObject>();
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
        SetActivePanel(_selectionPanel.name);
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
        {
            _playerListEntries = new Dictionary<int, GameObject>();
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            var entry = Instantiate(_playerListEntryPrefab);
            entry.transform.SetParent(_playerListContent.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);

            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(LobbyConstants.PlayerIsReady, out isPlayerReady))
            {
                entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);
            }

            _playerListEntries.Add(p.ActorNumber, entry);
        }

        _startGameButton.gameObject.SetActive(CheckPlayersReady());

        Hashtable props = new Hashtable
        {
            {LobbyConstants.PlayerLoadedLevel, false}
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
        var entry = Instantiate(_playerListEntryPrefab);
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

    #region UI CALLBACKS

    public void OnBackButtonClicked()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        SetActivePanel(_selectionPanel.name);
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = _roomNameInputField.text;
        roomName = (roomName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;

        byte maxPlayers;
        byte.TryParse(_maxPlayersInputField.text, out maxPlayers);
        maxPlayers = (byte) Mathf.Clamp(maxPlayers, 2, 8);

        RoomOptions options = new RoomOptions {MaxPlayers = maxPlayers, PlayerTtl = 10000};

        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public void OnJoinRandomRoomButtonClicked()
    {
        SetActivePanel(_joinRandomRoomPanel.name);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnLoginButtonClicked()
    {
        string playerName = _nickNameInputField.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public void OnRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }

        SetActivePanel(_roomListPanel.name);
    }

    public void OnSpecificRoomButtonClicked()
    {
        SetActivePanel(_specificRoomPanel.name);
        
        if (ProcessDeepLinkMngr.RoomName == null)
        {
            _specificRoomNameInputField.text = "";
            return;
        }
        _specificRoomNameInputField.text = ProcessDeepLinkMngr.RoomName;
    }

    public void OnStartGameButtonClicked()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PhotonNetwork.LoadLevel("GameOnline");
    }

    #endregion

    public void JoinSpecificRoom()
    {
        PhotonNetwork.JoinRoom(_specificRoomNameInputField.text);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneNames.Menu);
    }
    
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
    
    public void LocalPlayerPropertiesUpdated()
    {
        _startGameButton.gameObject.SetActive(CheckPlayersReady());
    }
    
    public void SetActivePanel(string activePanel)
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
