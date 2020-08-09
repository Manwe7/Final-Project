using UnityEngine;

namespace PlayerOfflineScipts
{
    public class JetPack : MonoBehaviour
    {
        [SerializeField] protected FixedJoystick _joystick;
        
        [SerializeField] protected Rigidbody2D _rigidbody2D;        
        
        [SerializeField] protected GameObject _fuelParticles;

        [SerializeField] protected FuelHandler _fuelHandler;

        [SerializeField] protected float _flySpeed;

        protected float _verticalMove;

        protected bool _isFlying => _verticalMove > 0.18f && _fuelHandler.HasFuel;

        private void Start()
        {    
            SetParticles(false);
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

        protected void SetDirections()
        {
            _verticalMove = _joystick.Vertical;
        }

        public virtual void ControlFuel()
        {        
            if (_isFlying)
            {
                _fuelHandler.SpendFuel();
                SetParticles(true);
            }
            else
            {
                _fuelHandler.RestoreFuel();
                SetParticles(false);
            }
        }

        protected void Fly()
        {                
            if (_isFlying)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _flySpeed);
            }        
        }

        protected void SetParticles(bool status)
        {
            _fuelParticles.SetActive(status);
        }
    }
}
