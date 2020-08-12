using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerMovement : PlayerOfflineScipts.PlayerMovement
    {
        [SerializeField] private PhotonView _photonView;

        private void Awake()
        {
            _joystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
        }

        private void Update()
        {
            if(!_photonView.IsMine) { return; }

            SetDirections();
        }

        private void FixedUpdate()
        {
            if(!_photonView.IsMine) { return; }

            HorizontalMovement();
        }
    }
}
