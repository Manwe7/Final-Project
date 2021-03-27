using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _pausePanel;

    [Header("Buttons")] 
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    private void Awake()
    {
        _exitButton.onClick.AddListener(LeaveRoom);
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeButton.onClick.AddListener(ResumeGame);
    }

    private void PauseGame()
    {
        _pausePanel.SetActive(true);
    }

    private void ResumeGame()
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
