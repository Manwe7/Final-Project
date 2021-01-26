using Multiplayer.Player;
using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerMovement : BasePlayer.PlayerMovement
    {
        [SerializeField] private PhotonView _photonView;

        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private void Awake()
        {
            if (_photonView.IsMine)
            {
                _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();

                _joystick = _playerOnlineProperties.MovementJoystick;
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
