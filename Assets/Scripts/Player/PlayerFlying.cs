using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerFlying : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick = null;    
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private Slider _playerFuelSlider = null;
    [SerializeField] private GameObject _fuelParticles = null;

    private float _flySpeed = 10;
    private float _maxfuelCapacity = 50;
    private float _verticalMove;
    private bool _shouldDelayFuelRestoring;
    private float _fuelCapacity;

    private void Start()
    {
        _shouldDelayFuelRestoring = false;
        _fuelParticles.SetActive(false);

        _fuelCapacity = _maxfuelCapacity;
        _playerFuelSlider.maxValue = _maxfuelCapacity;
    }

    private void Update()
    {
        SetDirections();
        ControlFuel();
    }

    private void FixedUpdate()
    {        
        Flying();
    }

    private void SetDirections()
    {
        _verticalMove = _joystick.Vertical;
    }

    private void ControlFuel()
    {
        _playerFuelSlider.value = _fuelCapacity;

        if (CanFly())
        {
            _fuelCapacity -= 0.1f;
            _fuelParticles.SetActive(true);
        }
        else
        {
            _fuelParticles.SetActive(false);

            if (IsRestoringFuel())
            {
                _fuelCapacity += 0.1f;
            }
        }

        if (CanRestoreFuel())
        {
            StartCoroutine(ReloadFuel());
        }
    }

    private void Flying()
    {                
        if (CanFly())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _flySpeed);
        }        
    }

    private bool CanFly()
    {
        return _verticalMove > 0.22f && _fuelCapacity > 0;
    }

    private bool IsRestoringFuel()
    { 
        return _fuelCapacity < _maxfuelCapacity && !_shouldDelayFuelRestoring;
    }

    private bool CanRestoreFuel()
    {
        return _fuelCapacity <= 0 && !_shouldDelayFuelRestoring;
    }

    private IEnumerator ReloadFuel()
    {
        _shouldDelayFuelRestoring = true;
        yield return new WaitForSeconds(1f);
        _shouldDelayFuelRestoring = false;
    }
}
