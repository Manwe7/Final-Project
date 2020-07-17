using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _movementJoystick = null;
    [SerializeField] private Slider _playerFuelSlider = null;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private GameObject fuelParticles = null;    

    private float _moveSpeed = 17;
    private float _flySpeed = 10;
    private float _maxfuelCapacity = 50;
    private float _horizontalMove, _verticalMove;
    private float _fuelCapacity;
    
    private bool _shouldDelayFuelRestoring;

    private void Start()
    {
        _shouldDelayFuelRestoring = false;
        fuelParticles.SetActive(false);

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
        HorizontalMovement();
        Flying();
    }

    #region Update methods
    private void SetDirections()
    {
        _horizontalMove = _movementJoystick.Horizontal;
        _verticalMove = _movementJoystick.Vertical;
    }

    private void ControlFuel()
    {
        _playerFuelSlider.value = _fuelCapacity;

        if (CanFly())
        {
            _fuelCapacity -= 0.1f;
            fuelParticles.SetActive(true);
        }
        else
        {
            fuelParticles.SetActive(false);

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
    #endregion

    #region FixedUpdate methods
    private void HorizontalMovement()
    {                
        _rigidbody2D.velocity = new Vector2(_horizontalMove * _moveSpeed, _rigidbody2D.velocity.y);
    }

    private void Flying()
    {                
        if (CanFly())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _flySpeed);
        }        
    }
    #endregion

    #region Bools
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
    #endregion

    private IEnumerator ReloadFuel()
    {
        _shouldDelayFuelRestoring = true;
        yield return new WaitForSeconds(1f);
        _shouldDelayFuelRestoring = false;
    }
}
