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
        }
        
        private void Start()
        {
            if (!_photonView.IsMine) { return; }

            SetParticles(false);
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            SetDirections();
            ControlFuel();
        }

        private void FixedUpdate()
        {     
            if (!_photonView.IsMine) { return; }
              
            Fly();
        }
        
        public override void ControlFuel()
        {        
            if (_isFlying)
            {
                _rigidbody2D.gravityScale = 0f;
                _fuelHandler.SpendFuel();
                _fuelParticles.SetActive(true);
            }
            else
            {
                _rigidbody2D.gravityScale = 5f;
                _fuelHandler.RestoreFuel();
                _fuelParticles.SetActive(false);
            }
        }
    }
}
