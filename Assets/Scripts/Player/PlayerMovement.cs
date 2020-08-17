using UnityEngine;

namespace PlayerOfflineScipts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] protected FixedJoystick _joystick = null;

        [SerializeField] protected Rigidbody2D _rigidbody2D = null;

        [SerializeField] protected PlayerSettings _playerSettings;

        protected float _horizontalMove;

        private void Update()
        {
            SetDirections();
        }

        private void FixedUpdate()
        {
            HorizontalMovement();
        }

        public virtual void SetDirections()
        {
            _horizontalMove = _joystick.Horizontal;
        }

        public virtual void HorizontalMovement()
        {                
            _rigidbody2D.velocity = new Vector2(_horizontalMove * _playerSettings._moveSpeed, _rigidbody2D.velocity.y);
        }
    }
}
