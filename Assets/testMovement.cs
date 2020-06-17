using UnityEngine;
using Photon.Pun;

public class testMovement : MonoBehaviour
{
    private Vector2 _pointA, _pointB;
    private bool directionRight;

    private Rigidbody2D _rigidbody2D;
    private PhotonView _photonView;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        _pointA = new Vector2(transform.position.x + 5f, transform.position.y);
        _pointB = new Vector2(transform.position.x - 5f, transform.position.y);
    }

    void Update()
    {
        if (!_photonView.IsMine) { return; }

        if (transform.position.x >= _pointA.x)
        { directionRight = false; }
        if (transform.position.x <= _pointB.x)
        { directionRight = true; }
        
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
        if (directionRight)
        {
            _rigidbody2D.velocity = Vector2.right * 5;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.left * 5;
        }
    }
}
