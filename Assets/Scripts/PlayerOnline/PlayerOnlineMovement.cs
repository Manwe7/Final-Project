using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerOnlineMovement : NetworkBehaviour
{
    [SerializeField] private GameObject fuelParticles = null;

    private float moveSpeed = 0, maxfuelCapacity = 0;   
    private FixedJoystick joystick = null;
    private Slider fuelSlider = null;

    private Rigidbody2D _rigidbody2D = null;

    private bool _reloadFuel;
    private float _horizontalMove, _verticalMove;
    private float _fuelCapacity;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        moveSpeed = 17;
        maxfuelCapacity = 50;

        joystick = GameObject.Find("Canvas/MovementJoystick ").GetComponent<FixedJoystick>();
        fuelSlider = GameObject.Find("Canvas/PlayerFuelSlider").GetComponent<Slider>();

        _reloadFuel = true;
        fuelParticles.SetActive(false);

        _fuelCapacity = maxfuelCapacity;
        fuelSlider.maxValue = maxfuelCapacity;
    }

    private void FixedUpdate()
    {
        //Movement
        _horizontalMove = joystick.Horizontal;

        _rigidbody2D.velocity = new Vector2(_horizontalMove * moveSpeed, _rigidbody2D.velocity.y);

        //Check for sides
        /*if (_horizontalMove > 0 && !_facingLeft)
        {
            Flip();
        }
        else if (_horizontalMove < 0 && _facingLeft)
        {
            Flip();
        }*/
    }

    private void Update()
    {
        fuelSlider.value = _fuelCapacity;
        _verticalMove = joystick.Vertical;

        if (_verticalMove > 0.22f && _fuelCapacity > 0)
        {
            fuelParticles.SetActive(true);
            _fuelCapacity -= 0.1f;
            _rigidbody2D.velocity = Vector2.up * 10;
        }
        else
        {
            fuelParticles.SetActive(false);
            if (_fuelCapacity < maxfuelCapacity && _reloadFuel)
            {
                _fuelCapacity += 0.1f;
            }
        }

        if (_fuelCapacity <= 0 && _reloadFuel == true)
        { StartCoroutine(ReloadFuel()); }
    }

    IEnumerator ReloadFuel()
    {
        _reloadFuel = false;
        yield return new WaitForSeconds(2f);
        _reloadFuel = true;
    }
}
