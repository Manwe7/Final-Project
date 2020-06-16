using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class OnlineManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject Player = null;

    private void Start()
    {
        PhotonNetwork.Instantiate(Player.name, Vector3.zero, Quaternion.identity);
    }

    public void LeaveRoom()
    { 
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Player " + newPlayer + " enter the game");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer + " left the game");
    }
}
