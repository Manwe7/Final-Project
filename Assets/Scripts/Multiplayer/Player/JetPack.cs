using Photon.Pun;
using UnityEngine;

namespace PlayerOnlineScripts
{
    public class JetPack : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;        
        
        [SerializeField] private GameObject _fuelParticles;

        [SerializeField] private FuelHandler _fuelHandler;

        [SerializeField] private float _flySpeed;

        private FixedJoystick _joystick;

        private float _verticalMove;

        private bool _isFlying => _verticalMove > 0.18f && _fuelHandler.HasFuel;

        private void Awake()
        {
            if(!_photonView.IsMine) { return ;}

            _joystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
        }
        
        private void Start()
        {
            if (!_photonView.IsMine) { return; }

            _fuelParticles.SetActive(false);                        
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

        private void SetDirections()
        {
            _verticalMove = _joystick.Vertical;
        }

        private void ControlFuel()
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

        private void Fly()
        {                
            if (_isFlying)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _flySpeed);
            }        
        }
    }
}
