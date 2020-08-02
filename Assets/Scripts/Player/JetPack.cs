using UnityEngine;

namespace PlayerOfflineScipts
{
    public class JetPack : MonoBehaviour
    {
        [SerializeField] private FixedJoystick _joystick;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;        
        
        [SerializeField] private GameObject _fuelParticles;

        [SerializeField] private FuelHandler _fuelHandler;

        [SerializeField] private float _flySpeed;

        private float _verticalMove;

        private bool _isFlying => _verticalMove > 0.18f && _fuelHandler.HasFuel;

        private void Start()
        {    
            _fuelParticles.SetActive(false);                        
        }

        private void Update()
        {
            SetDirections();
            ControlFuel();
        }

        private void FixedUpdate()
        {        
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
                _fuelHandler.SpendFuel();
                _fuelParticles.SetActive(true);
            }
            else
            {
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
