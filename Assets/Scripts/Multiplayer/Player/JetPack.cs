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

        public override void SetDirections()
        {
            if (!_photonView.IsMine) { return; }

            base.SetDirections();
        }
        
        public override void ControlFuel()
        {
            if (!_photonView.IsMine) { return; }

            base.ControlFuel();
        }

        public override void Fly()
        {
            if (!_photonView.IsMine) { return; }

            base.Fly();
        }
    }
}
