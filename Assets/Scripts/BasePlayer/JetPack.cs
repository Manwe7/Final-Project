using ScriptableObjects;
using UnityEngine;

namespace BasePlayer
{
    public class JetPack : MonoBehaviour
    {
        [SerializeField] protected Joystick _joystick;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] protected GameObject _fuelParticles;
        [SerializeField] protected FuelHandler _fuelHandler;
        [SerializeField] protected PlayerSettings _playerSettings;

        protected float _verticalMove;
        protected bool IsFlying => _verticalMove > 0.18f && _fuelHandler.HasFuel;

        private void Start()
        {    
            _fuelParticles.SetActive(IsFlying);
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

        protected virtual void SetDirections()
        {
            _verticalMove = _joystick.Vertical;
        }

        protected virtual void ControlFuel()
        {
            if (IsFlying)
            {
                _rigidbody2D.gravityScale = 0f;
                _fuelHandler.SpendFuel();
            }
            else
            {
                _rigidbody2D.gravityScale = 5f;
                _fuelHandler.RestoreFuel();
            }
            
            if(_fuelParticles.activeSelf && IsFlying || !_fuelParticles.activeSelf && !IsFlying) return;
            
            _fuelParticles.SetActive(IsFlying);
        }

        protected virtual void Fly()
        {                
            if (IsFlying)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _playerSettings._flySpeed);
            }
        }
    }
}
