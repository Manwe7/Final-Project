﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class OnlineManager : MonoBehaviourPunCallbacks
{    
    [SerializeField] private MapSections _mapSections;
 
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _pausePanel;

    private void Start()
    {
        int posX = Random.Range(-55, 55);
        int posy = Random.Range(-7, 25);
        
        GameObject player = PhotonNetwork.Instantiate(_player.name, new Vector2(posX, posy), Quaternion.identity);
        player.name = "Player" + PhotonNetwork.NickName;
    }

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