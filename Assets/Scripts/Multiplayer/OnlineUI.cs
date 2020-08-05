using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class OnlineUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _pausePanel;

    public void PauseGame()
    {
        _pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
    }

    public void LeaveRoom()
    { 
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
