using UnityEngine;
using UnityEngine.UI;

public class FuelHandler : MonoBehaviour
{
    [SerializeField] private Slider _playerFuelSlider;
    
    [SerializeField] private PlayerFlying _playerFlying;

    [HideInInspector] public float _maxfuelCapacity = 50;

    [HideInInspector] public float _fuelCapacity;

    private void Awake()
    {
        _playerFlying.OnFuelCapacityChange += SliderValue;   
    }

    private void Start()
    {        
        _fuelCapacity = _maxfuelCapacity;
        _playerFuelSlider.maxValue = _maxfuelCapacity;
        SliderValue(_fuelCapacity);
    }

    private void OnDestroy()
    {
        _playerFlying.OnFuelCapacityChange -= SliderValue;
    }

    private void SliderValue(float capacity)
    {
        _playerFuelSlider.value = _fuelCapacity;
    }
}
