using UnityEngine;
using Photon.Pun;

public class OnlineManager : MonoBehaviourPunCallbacks
{    
    [SerializeField] private MapSections _mapSections; //DOES NOT WORK
 
    [SerializeField] private GameObject _player;

    private void Start()
    {
        int posX = Random.Range(-55, 55);
        int posy = Random.Range(-7, 25);
        
        GameObject player = PhotonNetwork.Instantiate(_player.name, new Vector2(posX, posy), Quaternion.identity);
        player.name = "Player" + PhotonNetwork.NickName;
    }
}
