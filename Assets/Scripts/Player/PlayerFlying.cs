using UnityEngine;
using System.Collections;
using System;

public class PlayerFlying : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    
    [SerializeField] private Rigidbody2D _rigidbody2D;        
    
    [SerializeField] private GameObject _fuelParticles;

    [SerializeField] private FuelHandler _fuelHandler;

    [SerializeField] private float _flySpeed;

    private float _verticalMove;

    private bool _shouldDelayFuelRestoring;
    
    private void Start()
    {
        _shouldDelayFuelRestoring = false;
        _fuelParticles.SetActive(false);                        
    }

    private void Update()
    {
        SetDirections();
        ControlFuel();
    }

    private void FixedUpdate()
    {        
        Fly();
    }

    private void SetDirections()
    {
        _verticalMove = _joystick.Vertical;
    }

    private void ControlFuel()
    {        
        if (CanFly())
        {
            ChangeFuelCapacity(-0.1f);
            _fuelParticles.SetActive(true);
        }
        else
        {
            _fuelParticles.SetActive(false);

            if (IsRestoringFuel())
            {
                ChangeFuelCapacity(0.1f);
            }
        }

        if (CanRestoreFuel())
        {
            StartCoroutine(DelayRestoringFuel());
        }
    }

    private void ChangeFuelCapacity(float value)
    {        
        OnFuelCapacityChange?.Invoke(_fuelHandler._fuelCapacity += value);
    }

    private void Fly()
    {                
        if (CanFly())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalMove * _flySpeed);
        }        
    }

    private bool CanFly()
    {
        return _verticalMove > 0.18f && _fuelHandler._fuelCapacity > 0;
    }

    private bool IsRestoringFuel()
    { 
        return _fuelHandler._fuelCapacity < _fuelHandler._maxfuelCapacity && !_shouldDelayFuelRestoring;
    }

    private bool CanRestoreFuel()
    {
        return _fuelHandler._fuelCapacity <= 0 && !_shouldDelayFuelRestoring;
    }

    private IEnumerator DelayRestoringFuel()
    {
        _shouldDelayFuelRestoring = true;
        yield return new WaitForSeconds(1f);
        _shouldDelayFuelRestoring = false;
    }

    public event Action<float> OnFuelCapacityChange;
}
