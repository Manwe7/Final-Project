using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShowScoreOnline : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text[] _playerName;

    [SerializeField] private Text[] _playerScore;

    public const string LivesSaveKey = "RemainingLives";

    private int _remainingLives;

    private int _savedLives;

    public void Update()
    {        
        CheckPlayerHealth();
    }

    private void CheckPlayerHealth()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey(LivesSaveKey))
            {
                _remainingLives = (int)PhotonNetwork.PlayerList[i].CustomProperties[LivesSaveKey];
            }
            
            if(_remainingLives != _savedLives)
            {
                SetPlayerText();
            }

            _savedLives = _remainingLives;
        }
    }

    private void SetPlayerText()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            _playerName[i].text = PhotonNetwork.PlayerList[i].NickName;

            if(PhotonNetwork.PlayerList[i].CustomProperties[LivesSaveKey] != null)
            {
                _remainingLives = (int)PhotonNetwork.PlayerList[i].CustomProperties[LivesSaveKey];
                _playerScore[i].text = _remainingLives.ToString();
            }
        }
    }
}