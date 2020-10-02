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
    
    public void Initialize(string name, byte currentPlayers, byte maxPlayers)
    {
        _roomName = name;

        _roomNameText.text = name;
        _roomPlayersText.text = currentPlayers + " / " + maxPlayers;
    }
}
