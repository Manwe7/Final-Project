using UnityEngine;

namespace Weapon
{
    public class EnemyWeaponPosition : BaseWeaponPosition
    {    
        private GameObject _player;

        private void Start()
        {
            _offset = 270;
        }

        public void Init(GameObject player)
        {
            _player = player;
        }

        protected override void ChangeWeaponPosition()
        {
            if(_player == null) { return; }
        
            var playerPos = _player.transform.position;

            _direction = _player.transform.position - transform.position;
            _rotationZ = Mathf.Atan2(_direction.y,_direction.x) * Mathf.Rad2Deg;

            if(playerPos.x < transform.position.x) 
            {
                SetToLeftSide(-_rotationZ + _offset);
            } 
            else
            {
                SetToRightSide(_rotationZ + _offset);
            }
        }
    }
}
