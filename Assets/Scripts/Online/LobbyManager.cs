using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text LogText = null;
    [SerializeField] private InputField nicknameInput = null;

    private void Start()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }

    public void CreateRoom()
    {
        if (nicknameInput.text == null)
        {
            Debug.Log("Empty");
            return;
        }
        else
        {
            Debug.Log(nicknameInput.text);
            PhotonNetwork.NickName = nicknameInput.text;
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        }
    }

    public void RandomRoom()
    {
        if (nicknameInput.text == null)
        {
            Debug.Log("Empty");
            return;
        }
        else
        {
            Debug.Log(nicknameInput.text);
            PhotonNetwork.NickName = nicknameInput.text;
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected");

        PhotonNetwork.LoadLevel("GameOnline");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }

    //Exit button
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
