using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _createRoom, _randomRoom;

    [SerializeField] private Text _createRoomText, _randomRoomText;
    
    [Header("Nickname Input")]
    [SerializeField] private InputField _nicknameInput;

    [Header("Error panels")]
    [SerializeField] private GameObject NickNameError;

    [SerializeField] private GameObject NoRoomError;

    private byte _enableColor = 245;

    private byte _disableColor = 150;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        ButtonStatus(false ,_disableColor);
    }

    private void ButtonStatus(bool status, byte alhpa)
    {
        _createRoom.enabled = status;
        _randomRoom.enabled = status;

        _createRoomText.color = new Color32(0, 255, 255, alhpa);
        _randomRoomText.color = new Color32(0, 255, 255, alhpa);
    }

    private void EmptyNameError()
    {
        NickNameError.SetActive(true);
        StartCoroutine(TurnErrorsOff());
    }

    private IEnumerator TurnErrorsOff()
    {        
        yield return new WaitForSeconds(1.7f);
        NickNameError.SetActive(false);
        NoRoomError.SetActive(false);
    }

    private bool IsNameEmpty()
    {
        return _nicknameInput.text == "";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ButtonStatus(true, _enableColor);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameOnline");
    }

    #region Buttons
    public void CreateRoom()
    {
        if (IsNameEmpty())
        {
            EmptyNameError();
            return;
        }
        
        PhotonNetwork.NickName = _nicknameInput.text;

        int randomRoomName = Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 8 };

        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);        
    }

    public void RandomRoom()
    {
        if (IsNameEmpty())
        {
            EmptyNameError();
            return;
        }
        
        PhotonNetwork.NickName = _nicknameInput.text;
        PhotonNetwork.JoinRandomRoom();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion
    
    #region Network Errors
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        NoRoomError.SetActive(true);
        StartCoroutine(TurnErrorsOff());
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom(); //Try to create a new one again
    }
    #endregion
}
