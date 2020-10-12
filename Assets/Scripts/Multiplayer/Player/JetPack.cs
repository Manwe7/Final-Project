using Photon.Pun;
using UnityEngine;

namespace PlayerOnlineScripts
{
    public class JetPack : PlayerOfflineScipts.JetPack
    {
        [SerializeField] private PhotonView _photonView;
        
        private void Awake()
        {
            if(!_photonView.IsMine) { return; }

            _joystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();

            SetParticles(false);
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
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_playerSettings._flySpeed * 1.7f);
            }
        }
    }
}
