using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0;
    [SerializeField] private GameObject FuelParticles = null;
    [SerializeField] private Joystick _joystick = null;
    
    private Rigidbody2D _rigidbody2D = null;
    
    private bool _facingLeft = true;
    private float horizontalMove, verticalMove;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        //Movement
        horizontalMove = _joystick.Horizontal;
        
        _rigidbody2D.velocity = new Vector2(horizontalMove * MoveSpeed, _rigidbody2D.velocity.y);
        
        //Check for sides
        if (horizontalMove > 0 && !_facingLeft)
        {
            Flip();
        }
        else if (horizontalMove < 0 && _facingLeft)
        {
            Flip();
        }
    }

    private void Update()
    {
        verticalMove = _joystick.Vertical;
        if (verticalMove > 0.5f)
        {
            _rigidbody2D.velocity = Vector2.up * 10;
        }
    }

    private void Flip()
    {
        //Flip sides
        _facingLeft = !_facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}
