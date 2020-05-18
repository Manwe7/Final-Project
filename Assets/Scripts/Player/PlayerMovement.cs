using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0, jumpForce = 0;
    [SerializeField] private GameObject JumpParticles = null;
    [SerializeField] private Joystick _joystick;
    
    private Rigidbody2D _rigidbody2D = null;
    
    private bool _onGround, _doubleJump, _facingLeft = true;
    private float horizontalMove, verticalMove;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        
    }

    private void FixedUpdate()
    {
        //Movement
        horizontalMove = _joystick.Horizontal;//Input.GetAxis("Horizontal");
        

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
        //Jump or double Jump
        if (/*Input.GetKeyDown(KeyCode.Space)*/verticalMove > 0.5f && _onGround == true)
        { _onGround = false; Jump(); }
        else if (/*Input.GetKeyDown(KeyCode.Space)*/verticalMove > 0.5f && _doubleJump == true)
        { _doubleJump = false; Jump(); }
    }

    private void Flip()
    {
        //Flip sides
        _facingLeft = !_facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        Instantiate(JumpParticles, transform.position, transform.rotation);

        //Reset velocity on Y-axis
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);

        //Add force on Y-axis
        _rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If touched the ground, jump and double jumps are allowed again
        if (other.gameObject.CompareTag("Ground"))
        {
            _onGround = true;
            _doubleJump = true;
        }
    }
}
