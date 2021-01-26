using Multiplayer.Player;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerOnlineScripts
{
    public class FuelHandler : BasePlayer.FuelHandler
    {
        [SerializeField] private PhotonView _photonView;
        
        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private void Awake()
        {
            if(!_photonView.IsMine) { return ;}

            _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();

            _playerFuelSlider = _playerOnlineProperties.FuelSlider;
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