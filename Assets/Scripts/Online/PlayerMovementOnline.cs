using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

public class PlayerMovementOnline : MonoBehaviour
{
    [SerializeField] private GameObject fuelParticles = null;

    private FixedJoystick _fixedjoystick = null;
    private Slider _fuelSlider = null;
    private Rigidbody2D _rigidbody2D = null;

    private float moveSpeed = 17;
    private float maxfuelCapacity = 50;
    private bool _facingLeft = true, _reloadFuel;
    private float _horizontalMove, _verticalMove;
    private float _fuelCapacity;
    private PhotonView _photonView;

    private void Awake()
    {
        _fixedjoystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
        _fuelSlider = GameObject.Find("Canvas/PlayerFuelSlider").GetComponent<Slider>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _reloadFuel = true;
        fuelParticles.SetActive(false);

        _fuelCapacity = maxfuelCapacity;
        _fuelSlider.maxValue = maxfuelCapacity;
    }
    
    private void Update()
    {
        if (!_photonView.IsMine)
        { return; }

        _fuelSlider.value = _fuelCapacity;
        _verticalMove = _fixedjoystick.Vertical;
        _horizontalMove = _fixedjoystick.Horizontal;

        if (_verticalMove > 0.22f && _fuelCapacity > 0)
        {
            fuelParticles.SetActive(true);
            _fuelCapacity -= 0.1f;
        }
        else
        {
            //_rigidbody2D.gravityScale = 5;
            fuelParticles.SetActive(false);
            if (_fuelCapacity < maxfuelCapacity && _reloadFuel)
            {
                _fuelCapacity += 0.1f;
            }
        }

        //Check for sides
        if (_horizontalMove > 0 && !_facingLeft)
        {
            Flip();
        }
        else if (_horizontalMove < 0 && _facingLeft)
        {
            Flip();
        }

        if (_fuelCapacity <= 0 && _reloadFuel == true)
        { StartCoroutine(ReloadFuel()); }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
        {            
            //Movement
            _rigidbody2D.velocity = new Vector2(_horizontalMove * moveSpeed, _rigidbody2D.velocity.y);
            /*if (_horizontalMove > 0)
            { transform.Translate(Time.deltaTime * 10, 0, 0); }
            if (_horizontalMove < 0)
            { transform.Translate(-Time.deltaTime * 10, 0, 0); }
            */
            /*if (flying)
            { _rigidbody2D.velocity = Vector2.up * 10; }*/

            if (_verticalMove > 0.22f && _fuelCapacity > 0)
            {
                //_rigidbody2D.velocity = Vector2.up * 10; 
                transform.Translate(0, Time.deltaTime * 8, 0);
                _rigidbody2D.gravityScale = 0;
            }
            else
            {
                _rigidbody2D.gravityScale = 5;
            }
        }
    }

    IEnumerator ReloadFuel()
    {
        _reloadFuel = false;
        yield return new WaitForSeconds(2f);
        _reloadFuel = true;
    }

    private void Flip()
    {
        //Flip sides
        /*_facingLeft = !_facingLeft;
        transform.Rotate(0f, 180f, 0f);*/
    }
}
