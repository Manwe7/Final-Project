using Photon.Pun;
using UnityEngine;

namespace PlayerOnlineScripts
{
    public class PlayerWeaponPosition : BaseWeaponPosition
    {
        [SerializeField] private PhotonView _photonView;
        
        private Joystick _weaponJoystick;

        private RectTransform _joystickHandle;

        private void Awake()
        {        
            if (!_photonView.IsMine) { return; }

            _weaponJoystick = GameObject.Find("Canvas/RotationJoystick").GetComponent<FixedJoystick>();
            _joystickHandle = GameObject.Find("Canvas/RotationJoystick/Handle").GetComponent<RectTransform>();
        }

        private void Start()
        {
            _offset = 180;
        }

        protected override void ChangeWeaponPosition()
        {
            if (!_photonView.IsMine) { return; }
            
            _direction = Vector3.up * _weaponJoystick.Vertical + Vector3.right * _weaponJoystick.Horizontal;
            _rotationZ = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

            if(_rotationZ == 0) return;

            if (_joystickHandle.anchoredPosition.x < 0)
            {
                SetToLeftSide();
            }
            else if (_joystickHandle.anchoredPosition.x > 0)
            {
                SetToRightSide();
            }
        }

        private void SetToRightSide()
        {       
            transform.rotation = Quaternion.Euler(0f, 0f, -_rotationZ);
            transform.position = GetPosition(0.3f);
        }

        private void SetToLeftSide()
        {
            transform.rotation = Quaternion.Euler(-180f, 0f, _rotationZ + _offset);
            transform.position = GetPosition(-0.3f);
        }
    }
}
