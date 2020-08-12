using Photon.Pun;
using UnityEngine;

namespace PlayerOnlineScripts
{
    public class PlayerWeaponPosition : PlayerOfflineScipts.PlayerWeaponPosition
    {
        [SerializeField] private PhotonView _photonView;

        private void Awake()
        {        
            if (!_photonView.IsMine) { return; }

            _weaponJoystick = GameObject.Find("Canvas/RotationJoystick").GetComponent<FixedJoystick>();
            _joystickHandle = GameObject.Find("Canvas/RotationJoystick/Handle").GetComponent<RectTransform>();
        }

        protected override void ChangeWeaponPosition()
        {
            if (!_photonView.IsMine) { return; }
            
            base.ChangeWeaponPosition();
        }

    }
}
