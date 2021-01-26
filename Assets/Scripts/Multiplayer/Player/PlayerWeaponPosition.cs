using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Player
{
    public class PlayerWeaponPosition : BasePlayer.Weapon.PlayerWeaponPosition
    {
        [SerializeField] private PhotonView _photonView;

        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private void Awake()
        {        
            if (!_photonView.IsMine) { return; }

            _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();
            
            _weaponJoystick = _playerOnlineProperties.WeaponJoystick;
            _joystickHandle = _playerOnlineProperties.JoystickHandle;
        }

        protected override void ChangeWeaponPosition()
        {
            if (!_photonView.IsMine) { return; }
            
            base.ChangeWeaponPosition();
        }

    }
}
