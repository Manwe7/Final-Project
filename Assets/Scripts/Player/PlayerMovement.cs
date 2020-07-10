using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject fuelParticles = null;    
    
    private FixedJoystick _fixedjoystick = null;
    private Slider _fuelSlider = null;    
    private Rigidbody2D _rigidbody2D = null;

    private float _moveSpeed = 17;
    private float _maxfuelCapacity = 50;
    private bool  _reloadFuel;
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

        _fuelCapacity = _maxfuelCapacity;
        _fuelSlider.maxValue = _maxfuelCapacity;
    }
    
    private void Update()
    {
        Flying();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //Horizontal Movement
        _horizontalMove = _fixedjoystick.Horizontal;
        _rigidbody2D.velocity = new Vector2(_horizontalMove * _moveSpeed, _rigidbody2D.velocity.y);
    }

    private void Flying()
    {
        //Slider value
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
            if (_fuelCapacity < _maxfuelCapacity && _reloadFuel)
            {
                _fuelCapacity += 0.1f;
            }
        }

        if (_fuelCapacity <= 0 && _reloadFuel == true)
        { StartCoroutine(ReloadFuel()); }
    }

    private IEnumerator ReloadFuel()
    {        
        _reloadFuel = false;
        yield return new WaitForSeconds(2f);
        _reloadFuel = true;
    }
}
