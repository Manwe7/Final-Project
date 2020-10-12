using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerMovement : PlayerOfflineScipts.PlayerMovement
    {
        [SerializeField] private PhotonView _photonView;

        private void Awake()
        {
            if (_photonView.IsMine)
            {
                _joystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
            }
        }
        
        protected override void SetDirections()
        {
            if(_photonView.IsMine)
            {
                base.SetDirections();
            }
        }
        
        protected override void HorizontalMovement()
        {
            if(_photonView.IsMine)
            {
                base.HorizontalMovement();
            }
        }
    }
}
