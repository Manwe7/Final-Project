using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerWeapon : BaseWeapon
    {
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private Transform _parent;

        [SerializeField] private GameObject _bulletOnline;

        private void OnEnable()
        {
            _reloadTime = 0.3f;
            _reloaded = true;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            if (_reloaded)
            {
                ShootBullet();
                StartCoroutine(Reload());
            }
        }
        
        [PunRPC]
        public void ShootBullet()
        {
            GameObject bullet = PhotonNetwork.Instantiate(_bulletOnline.name, _barrel.position, _barrel.rotation);
            bullet.name = _parent.gameObject.name + "Bullet";
        }        
    }
}
