using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerHealth : PlayerOfflineScipts.PlayerHealth
    {
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private PlayerOnlineScripts.Player _playerOnline;

        private void Awake()
        {
            if (!_photonView.IsMine) { return; }

            _healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();

            _playerOnline.OnRespawn += SetLifeProperties;
        }

        public override void SetLifeProperties()
        {
            if (!_photonView.IsMine) { return; }

            base.SetLifeProperties();
        }

        private void OnDestroy()
        {
            _playerOnline.OnRespawn -= SetLifeProperties;
        }

        private void ChangeHealth()
        {
            if (!_photonView.IsMine) { return; }

            _healthSlider.value = _health;
            if (_health <= 0)
            {
                _photonView.RPC("GetKilled", RpcTarget.AllViaServer);
            }
        }

        public void GetDamage(int damage)
        {
            if (_photonView.IsMine)
            {
                _health -= damage;
            }
            ChangeHealth();
        }

        public override void GetKilled()
        {
            if (!_photonView.IsMine) { return; }

            base.GetKilled();
        }
    }
}