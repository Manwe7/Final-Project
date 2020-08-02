using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private Transform parent, barrel;

        [SerializeField] private GameObject BulletOnline;
        
        [SerializeField] private float reloadTime = 0;

        private bool _reloaded;

        private void Start()
        {
            _reloaded = true;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            if (_reloaded)
            {
                Shoot();
                _reloaded = false;
                Invoke("Reload", reloadTime);
            }

        }
        
        [PunRPC]
        public void Shoot()
        {
            GameObject bullet = PhotonNetwork.Instantiate(BulletOnline.name, barrel.position, barrel.rotation);
            bullet.name = parent.gameObject.name + "Bullet";
        }

        private void Reload()
        {
            _reloaded = true;
        }
    }
}
