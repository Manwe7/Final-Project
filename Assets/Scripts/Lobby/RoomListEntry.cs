using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RoomListEntry : MonoBehaviour
{
    [SerializeField] private Text _roomNameText;
    [SerializeField] private Text _roomPlayersText;
    [SerializeField] private Button _joinRoomButton;

    private string _roomName;
    
    public void Start()
    {
        _joinRoomButton.onClick.AddListener(() =>
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }

            PhotonNetwork.JoinRoom(_roomName);
        });
    }
    
    public void Initialize(string playerName, byte currentPlayers, byte maxPlayers)
    {
        _roomName = playerName;

        _roomNameText.text = playerName;
        _roomPlayersText.text = currentPlayers + " / " + maxPlayers;
    }
}
