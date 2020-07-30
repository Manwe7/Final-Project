using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class ShowScoreOnline : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text[] _playerName;

    [SerializeField] private Text[] _playerScore;

    public Player _player { get; private set; }

    public const string healthSave = "RemainingHealth";

    public void Update()
    {
        SetPlayerText();
    }

    // public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    // {
    //     if(targetPlayer != null)
    //     {
    //         SetPlayerText();
    //     }
    // }

    private void SetPlayerText()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            _playerName[i].text = PhotonNetwork.PlayerList[i].NickName;

            if(PhotonNetwork.PlayerList[i].CustomProperties[healthSave] != null)
            {
                int remainingLive = (int)PhotonNetwork.PlayerList[i].CustomProperties[healthSave];
                _playerScore[i].text = remainingLive.ToString();
            }
        }
    }   
}