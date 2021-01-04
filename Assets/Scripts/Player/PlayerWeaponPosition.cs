using UnityEngine;

namespace PlayerOfflineScipts
{
    public class PlayerWeaponPosition : BaseWeaponPosition
    {
        [SerializeField] protected Joystick _weaponJoystick;
        [SerializeField] protected RectTransform _joystickHandle;

        protected void Start()
        {
            _offset = 180;
        }

        protected override void ChangeWeaponPosition()
        {
            _direction = Vector3.up * _weaponJoystick.Vertical + Vector3.right * _weaponJoystick.Horizontal;
            _rotationZ = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

            if(_rotationZ == 0) return;

            if (_joystickHandle.anchoredPosition.x < 0)
            {
                SetToLeftSide(_rotationZ + _offset);
            }
            else if (_joystickHandle.anchoredPosition.x > 0)
            {
                SetToRightSide(-_rotationZ);
            }
        }
    }
}
