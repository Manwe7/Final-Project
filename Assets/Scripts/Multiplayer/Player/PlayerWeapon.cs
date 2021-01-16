using UnityEngine;
using Photon.Pun;
using Weapon;

namespace PlayerOnlineScripts
{
    public class PlayerWeapon : BaseWeapon
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private GameObject _bulletOnline;

        private void OnEnable()
        {
            _reloadTime = 0.5f;
            _reloaded = true;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            if (!_reloaded) return;
            
            _photonView.RPC("ShootBullet", RpcTarget.All);
            StartCoroutine(Reload());
        }
        
        [PunRPC]
        public void ShootBullet()
        {
            if (!_photonView.IsMine) return;
            
            var bullet = PhotonNetwork.Instantiate(_bulletOnline.name, _barrel.position, _barrel.rotation);
            bullet.GetComponent<Bullet>().Init(_photonView.Owner);
        }        
    }
}
