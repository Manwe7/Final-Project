using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerOnlineScripts
{
    public class FuelHandler : PlayerOfflineScipts.FuelHandler
    {
        [SerializeField] private PhotonView _photonView;
        
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
    }
}