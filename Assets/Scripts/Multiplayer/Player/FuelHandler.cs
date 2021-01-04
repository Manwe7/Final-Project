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

        protected override void SetFuelProperties()
        {
            if (!_photonView.IsMine) { return; }

            base.SetFuelProperties();
        }

        protected override void CheckFuelValues()
        {
            if (!_photonView.IsMine) { return; }

            base.CheckFuelValues();
        }
    }
}