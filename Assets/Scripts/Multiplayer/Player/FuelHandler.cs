using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerOnlineScripts
{
    public class FuelHandler : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;
        
        private Slider _playerFuelSlider;
    
        private float _maxfuelCapacity = 30;
        
        private float _fuelCapacity;

        private bool _shouldDelayFuelRestoring;

        private bool isFlying;
        
        private bool _isRestoringFuel => _fuelCapacity < _maxfuelCapacity && !_shouldDelayFuelRestoring;

        private bool _delayRestoreFuel => _fuelCapacity <= 0 && !_shouldDelayFuelRestoring;

        public bool HasFuel => _fuelCapacity > 0;

        private void Awake()
        {
            if(!_photonView.IsMine) { return ;}

            _playerFuelSlider = GameObject.Find("Canvas/PlayerFuelSlider").GetComponent<Slider>();
        }

        private void Start()
        {
            if (!_photonView.IsMine) { return; }

            _fuelCapacity = _maxfuelCapacity;
            _playerFuelSlider.maxValue = _maxfuelCapacity;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            if (_delayRestoreFuel)
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
            if (!_photonView.IsMine) { return; }
            
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