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

        public override void SetDirections()
        {
            if(_photonView.IsMine)
            {
                base.SetDirections();
            }
        }

        public override void HorizontalMovement()
        {
            if(_photonView.IsMine)
            {
                base.HorizontalMovement();
            }
        }
    }
}
