using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _createRoom, _specificRoom, _randomRoom;

    [SerializeField] private Text _createRoomText, _specificRoomText, _randomRoomText;
    
    [Header("Nickname Input")]
    [SerializeField] private InputField _nicknameInput;

    [SerializeField] private InputField _roomNameInput;

    [Header("Error panels")]
    [SerializeField] private GameObject _nickNameError;

    [SerializeField] private GameObject _roomNameError;

    [SerializeField] private GameObject _noRoomError;

    private byte _enableColor = 245;

    private byte _disableColor = 150;

    private void Start()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        ButtonStatus(false, _disableColor);
    }

    private void ButtonStatus(bool status, byte alhpa)
    {
        _createRoom.enabled = status;
        _randomRoom.enabled = status;
        _specificRoom.enabled = status;

        _createRoomText.color = new Color32(0, 255, 255, alhpa);
        _randomRoomText.color = new Color32(0, 255, 255, alhpa);
        _specificRoomText.color = new Color32(0, 255, 255, alhpa);
    }

    private IEnumerator TurnErrorsOff()
    {        
        yield return new WaitForSeconds(1.7f);
        _nickNameError.SetActive(false);
        _roomNameError.SetActive(false);
        _noRoomError.SetActive(false);
    }

    private bool IsNickNameEmpty()
    {
        return _nicknameInput.text == "";
    }

    private bool IsRoomNameEmpty()
    {
        return _roomNameInput.text == "";
    }

    private void EmptyNickNameError()
    {
        _nickNameError.SetActive(true);
        StartCoroutine(TurnErrorsOff());
    }

    private void EmptyRoomNameError()
    {
        _roomNameError.SetActive(true);
        StartCoroutine(TurnErrorsOff());
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
        if (IsNickNameEmpty())
        {
            EmptyNickNameError();
            return;
        }

        if(IsRoomNameEmpty())
        {
            EmptyRoomNameError();
            return;
        }
        
        PhotonNetwork.NickName = _nicknameInput.text;
        PhotonNetwork.CreateRoom(_roomNameInput.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void RandomRoom()
    {
        if (IsNickNameEmpty())
        {
            EmptyNickNameError();
            return;
        }

        PhotonNetwork.NickName = _nicknameInput.text;
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinSpecificRoom()
    {
        if (IsNickNameEmpty())
        {
            EmptyNickNameError();
            return;
        }

        if(IsRoomNameEmpty())
        {
            EmptyRoomNameError();
            return;
        }

        PhotonNetwork.NickName = _nicknameInput.text;
        PhotonNetwork.JoinRoom(_roomNameInput.text);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion

    #region Network Errors
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _noRoomError.SetActive(true);
        StartCoroutine(TurnErrorsOff());
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    #endregion
}
