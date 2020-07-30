using System.Collections;
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

    private bool _canRestoreFuel => _fuelCapacity <= 0 && !_shouldDelayFuelRestoring;

    public bool HasFule => _fuelCapacity > 0;
    

    private void Start()
    {        
        _fuelCapacity = _maxfuelCapacity;
        _playerFuelSlider.maxValue = _maxfuelCapacity;
    }

    private void Update()
    {
        if (_canRestoreFuel)
        {
            StartCoroutine(DelayRestoringFuel());
        }

        SliderValue(_fuelCapacity);
    }

    private void SliderValue(float capacity)
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
        if(_isRestoringFuel)
        {
            _fuelCapacity += 0.1f;
        }
    }

    public void SpendFuel()
    {
        _fuelCapacity -= 0.1f;
    }    
}
