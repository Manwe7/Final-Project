using UnityEngine;
using Photon.Pun;

public class AutoDestroyOnline : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    private void OnEnable()
    {
        Invoke(nameof(SelfDestroy), 4f);
    }

    private void SelfDestroy()
    {
        if (_photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
