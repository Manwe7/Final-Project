using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
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

    private void Awake()
    {
        _fixedjoystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
        _fuelSlider = GameObject.Find("Canvas/PlayerFuelSlider").GetComponent<Slider>();
        _rigidbody2D = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    {
        _reloadFuel = true;
        fuelParticles.SetActive(false);

        _fuelCapacity = maxfuelCapacity;
        _fuelSlider.maxValue = maxfuelCapacity;
    }

    private void FixedUpdate()
    {
        //Movement
        _horizontalMove = _fixedjoystick.Horizontal;
        
        _rigidbody2D.velocity = new Vector2(_horizontalMove * moveSpeed, _rigidbody2D.velocity.y);
        
        //Check for sides
        if (_horizontalMove > 0 && !_facingLeft)
        {
            Flip();
        }
        else if (_horizontalMove < 0 && _facingLeft)
        {
            Flip();
        }
    }

    private void Update()
    {
        _fuelSlider.value = _fuelCapacity;
        _verticalMove = _fixedjoystick.Vertical;

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

    private void Flip()
    {
        //Flip sides
        _facingLeft = !_facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}
