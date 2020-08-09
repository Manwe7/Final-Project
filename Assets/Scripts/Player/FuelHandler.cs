using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerOfflineScipts
{
    public class FuelHandler : MonoBehaviour
    {
        [SerializeField] protected Slider _playerFuelSlider;
        
        protected float _maxfuelCapacity = 30;
        
        protected float _fuelCapacity;

        protected bool _shouldDelayFuelRestoring;
                
        
        protected bool _isRestoringFuel => _fuelCapacity < _maxfuelCapacity && !_shouldDelayFuelRestoring;

        protected bool _delayRestoreFuel => _fuelCapacity <= 0 && !_shouldDelayFuelRestoring;

        public bool HasFuel => _fuelCapacity > 0;

        private void Awake()
        {
            _maxfuelCapacity = 30;
        }

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
        }

        protected void SetSliderValue(float capacity)
        {
            _playerFuelSlider.value = _fuelCapacity;
        }

        protected IEnumerator DelayRestoringFuel()
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
}