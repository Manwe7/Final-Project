using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick = null;    
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    private float _moveSpeed = 17;
    private float _horizontalMove;

    private void Update()
    {
        SetDirections();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void SetDirections()
    {
        _horizontalMove = _joystick.Horizontal;
    }

    private void HorizontalMovement()
    {                
        _rigidbody2D.velocity = new Vector2(_horizontalMove * _moveSpeed, _rigidbody2D.velocity.y);
    }
}
