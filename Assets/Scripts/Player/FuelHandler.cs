using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelHandler : MonoBehaviour
{
    [SerializeField] private Slider _playerFuelSlider;
    
    private float _maxfuelCapacity = 30;
    
    private float _fuelCapacity;

    private bool _shouldDelayFuelRestoring;

    private bool isFlying;
    
    
    private bool _isRestoringFuel => _fuelCapacity < _maxfuelCapacity && !_shouldDelayFuelRestoring;

    private bool _delayRestoreFuel => _fuelCapacity <= 0 && !_shouldDelayFuelRestoring;

    public bool HasFuel => _fuelCapacity > 0;

    //private Queue<Action> _fuelTasks = new Queue<Action>();

    private void Start()
    {        
        _fuelCapacity = _maxfuelCapacity;
        _playerFuelSlider.maxValue = _maxfuelCapacity;
    }

    private void Update()
    {
        if (_delayRestoreFuel)
        {
            StartCoroutine(DelayRestoringFuel());
        }

        SetSliderValue(_fuelCapacity);

        //Debug.Log(_fuelCapacity);

        // if(_fuelTasks.Count == 0)
        // {
        //     RestoreFuel();
        // }
        // else
        // {
        //     var currentTask = _fuelTasks.Dequeue();
        //     currentTask();
        // }
    }

    private void SetSliderValue(float capacity)
    {
        _playerFuelSlider.value = _fuelCapacity;
    }

    private IEnumerator DelayRestoringFuel()
    {
        _shouldDelayFuelRestoring = true;
        yield return new WaitForSeconds(1f);
        _shouldDelayFuelRestoring = false;
    }

    public void RestoreFuel()
    {
        Debug.Log($"is restoring {_isRestoringFuel} {_shouldDelayFuelRestoring} {_fuelCapacity}");
        if(_isRestoringFuel)
        {
            _fuelCapacity += 0.1f;
        }
    }

    public void SpendFuel()
    {
        _fuelCapacity -= 0.1f;
        //_fuelTasks.Enqueue(() => _fuelCapacity -= 0.1f);
    }    
}
