using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShowScoreOnline : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text[] _playerName;

    [SerializeField] private Text _player0Score;

    [SerializeField] private Text _player1Score;

    public void Start()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            _playerName[i].text = PhotonNetwork.PlayerList[i].NickName;
        }        
    }
}
