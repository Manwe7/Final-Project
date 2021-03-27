using BaseClasses;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Player
{
    public class PlayerWeapon : BaseWeapon
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private GameObject _bulletOnline;

        [Header("Stats")]
        [SerializeField] private float _handleOffsetToShoot;
        
        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private Joystick _weaponJoystick;
        
        private float _vertical, _horizontal;
        
        private void Awake()
        {        
            if (!_photonView.IsMine) { return; }

            _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();
            
            _weaponJoystick = _playerOnlineProperties.WeaponJoystick;
        }
        
        private void OnEnable()
        {
            _reloadTime = 0.5f;
            _reloaded = true;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }
            
            _vertical = _weaponJoystick.Vertical;
            _horizontal = _weaponJoystick.Horizontal;
            
            if (!_reloaded) return;
            
            if(Mathf.Abs(_vertical) > _handleOffsetToShoot || Mathf.Abs(_horizontal) > _handleOffsetToShoot)
            {
                _photonView.RPC("ShootBullet", RpcTarget.All);
                StartCoroutine(Reload());
            }
        }
        
        [PunRPC]
        public void ShootBullet()
        {
            if (!_photonView.IsMine) return;
            
            GameObject bullet = PhotonNetwork.Instantiate(_bulletOnline.name, _barrel.position, _barrel.rotation);
            bullet.GetComponent<PlayerOnlineScripts.Bullet>().Init(_photonView.Owner);
        }        
    }
}
