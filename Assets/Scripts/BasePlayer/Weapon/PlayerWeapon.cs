using BaseClasses;
using UnityEngine;

namespace BasePlayer.Weapon
{
    public class PlayerWeapon : BaseWeapon
    {
        [Header("Scripts")]
        [SerializeField] private Joystick _weaponJoystick;
        
        [Header("Stats")]
        [SerializeField] private float _handleOffsetToShoot;
        
        private float _vertical, _horizontal;
        
        private void OnEnable()
        {
            _reloadTime = 0.3f;
            _isReloaded = true;
        }

        private void Update()
        {        
            _vertical = _weaponJoystick.Vertical;
            _horizontal = _weaponJoystick.Horizontal;
            
            if(Mathf.Abs(_vertical) > _handleOffsetToShoot || Mathf.Abs(_horizontal) > _handleOffsetToShoot)
            {
                Shoot();
            }
        }
    }
}