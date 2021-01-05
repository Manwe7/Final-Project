using UnityEngine;
using Photon.Pun;

public class OnlineManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject[] _spawnPoints;

    private void Start()
    {
        var player = PhotonNetwork.Instantiate(_player.name, ChoosePos(), Quaternion.identity);
        player.name = "Player " + PhotonNetwork.NickName;
    }

    public Vector2 ChoosePos()
    {
        int point = Random.Range(0, _spawnPoints.Length);
        var positionToSpawn = _spawnPoints[point].transform.position;
        
        return positionToSpawn;
    }
}
