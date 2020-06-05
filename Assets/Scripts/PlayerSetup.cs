using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private PlayerOnline playerOnline = null;
    [SerializeField] private PlayerOnlineMovement playerOnlineMovement = null;
    [SerializeField] private PlayerOnlineWeapon playerOnlineWeapon = null;
    [SerializeField] private GameObject cameraPlayer = null;
    [SerializeField] private string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    private void Start()
    {
        string _ID = "Player" + GetComponent<NetworkIdentity>().netId;

        gameObject.name = _ID;

        if (!isLocalPlayer)
        {
            playerOnline.enabled = false;
            playerOnlineMovement.enabled = false;
            playerOnlineWeapon.enabled = false;
            cameraPlayer.SetActive(false);

            gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }   

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
