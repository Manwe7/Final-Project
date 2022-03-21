using Multiplayer.Player;
using Photon.Pun;
using UnityEngine;

namespace PlayerOnlineScripts
{
    public class JetPack : BasePlayer.JetPack
    {
        [SerializeField] private PhotonView _photonView;
        
        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private void Awake()
        {
            if(!_photonView.IsMine) { return; }

            _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();
            
            _joystick = _playerOnlineProperties.MovementJoystick;
        }

        protected override void SetDirections()
        {
            if (!_photonView.IsMine) { return; }

            base.SetDirections();
        }

        protected override void ControlFuel()
        {
            if (!_photonView.IsMine) { return; }

            base.ControlFuel();
        }

        protected override void Fly()
        {
            if (!_photonView.IsMine) { return; }

            //base.Fly(); //Sync between players is not stable with Gravity ON
            if (IsFlying)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _playerSettings._flySpeed);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_playerSettings._flySpeed * 1.1f);
            }
        }
    }
}
