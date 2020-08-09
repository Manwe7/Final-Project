using UnityEngine;

namespace PlayerOfflineScipts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] protected FixedJoystick _joystick = null;

        [SerializeField] protected Rigidbody2D _rigidbody2D = null;

        [SerializeField] protected float _moveSpeed;

        protected float _horizontalMove;

        private void Update()
        {
            SetDirections();
        }

        private void FixedUpdate()
        {
            HorizontalMovement();
        }

        protected void SetDirections()
        {
            _horizontalMove = _joystick.Horizontal;
        }

        protected void HorizontalMovement()
        {                
            _rigidbody2D.velocity = new Vector2(_horizontalMove * _moveSpeed, _rigidbody2D.velocity.y);
        }
    }
}
