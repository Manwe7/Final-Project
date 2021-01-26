using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BasePlayer
{
    public class FuelHandler : MonoBehaviour
    {
        [SerializeField] protected Slider _playerFuelSlider;

        private float _maxFuelCapacity = 30;
        private float _fuelCapacity;
        private bool _shouldDelayFuelRestoring;

        private bool IsRestoringFuel => _fuelCapacity < _maxFuelCapacity && !_shouldDelayFuelRestoring;
        private bool DelayRestoreFuel => _fuelCapacity <= 0 && !_shouldDelayFuelRestoring;

        public bool HasFuel => _fuelCapacity > 0;

        private void Start()
        {        
            SetFuelProperties();
        }

        protected virtual void SetFuelProperties()
        {
            _fuelCapacity = _maxFuelCapacity;
            _playerFuelSlider.maxValue = _maxFuelCapacity;
        }

        private void Update()
        {
            CheckFuelValues();
        }

        protected virtual void CheckFuelValues()
        {
            if (DelayRestoreFuel)
            {
                StartCoroutine(DelayRestoringFuel());
            }

            SetSliderValue(_fuelCapacity);
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
            if(IsRestoringFuel)
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