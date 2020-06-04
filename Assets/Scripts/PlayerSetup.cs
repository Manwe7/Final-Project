using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private PlayerOnline playerOnline = null;
    [SerializeField] private PlayerOnlineMovement playerOnlineMovement = null;
    [SerializeField] private GameObject cameraPlayer = null;

    Camera sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            playerOnline.enabled = false;
            playerOnlineMovement.enabled = false;
            cameraPlayer.SetActive(false);
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
