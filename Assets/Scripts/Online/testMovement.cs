using UnityEngine;
using Photon.Pun;

public class testMovement : MonoBehaviour
{
    private FixedJoystick _fixedjoystick = null;

    /*private Vector2 _pointA, _pointB;
    private bool directionRight;*/

    private Rigidbody2D _rigidbody2D;
    private PhotonView _photonView;

    private float _horizontalMove, _verticalMove;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();

        _fixedjoystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
    }    

    void Start()
    {
        /*_pointA = new Vector2(transform.position.x + 5f, transform.position.y);
        _pointB = new Vector2(transform.position.x - 5f, transform.position.y);*/
    }

    void Update()
    {
        if (!_photonView.IsMine) { return; }

        //This type of movement works fine!!!!!!!!!!
        /*if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(-Time.deltaTime * 5, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Time.deltaTime * 5, 0, 0);*/

        _horizontalMove = _fixedjoystick.Horizontal;
        _verticalMove = _fixedjoystick.Vertical;

        //_rigidbody2D.velocity = new Vector2(_horizontalMove * 10, _rigidbody2D.velocity.y);
        if (_horizontalMove > 0)
        { transform.Translate(Time.deltaTime * 5, 0, 0); }
        if (_horizontalMove < 0)
        { transform.Translate(-Time.deltaTime * 5, 0, 0); }

        if (_verticalMove > 0.22f)
        {
            //_rigidbody2D.velocity = Vector2.up * 10; 
            transform.Translate(0, Time.deltaTime * 5, 0);
            _rigidbody2D.gravityScale = 0;
        }
        else
        {
            _rigidbody2D.gravityScale = 5;
        }

















        //This type of movement works fine !!!!!!!!!!!!!
        /*if (transform.position.x >= _pointA.x)
        { directionRight = false; }
        if (transform.position.x <= _pointB.x)
        { directionRight = true; }*/

        //Position        
        /*if (directionRight)
        {         
            transform.position = transform.position + new Vector3(0.2f, 0, 0);
        }
        else
        {            
            transform.position = transform.position + new Vector3(-0.2f, 0, 0);
        }*/

        //Translate
        /*if (directionRight)
        {
            transform.Translate(Vector2.right * 5f * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * 5f * Time.deltaTime);
        }*/

        //Velocity
        /*if (directionRight)
        {
            _rigidbody2D.velocity = Vector2.right * 5;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.left * 5;
        }*/
    }
}
