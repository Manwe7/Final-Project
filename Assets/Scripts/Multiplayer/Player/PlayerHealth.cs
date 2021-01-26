using Multiplayer.Player;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerHealth : BasePlayer.PlayerHealth
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private Player _playerOnline;

        [TagSelector]
        [SerializeField] private string _propertiesTag;
        
        private PlayerOnlineProperties _playerOnlineProperties;
        
        private void Awake()
        {
            if (!_photonView.IsMine) return;

            _playerOnlineProperties = GameObject.FindWithTag(_propertiesTag).GetComponent<PlayerOnlineProperties>();
            
            _healthSlider = _playerOnlineProperties.HealthSlider;
            _cameraShake = _playerOnlineProperties.Camera.GetComponent<CameraShake>();

            _playerOnline.OnRespawn += SetLifeProperties;
        }

        protected override void SetLifeProperties()
        {
            if (!_photonView.IsMine) return;

            base.SetLifeProperties();
        }

        private void OnDestroy()
        {
            _playerOnline.OnRespawn -= SetLifeProperties;
        }

        protected override void ChangeHealth(float health)
        {
            if (!_photonView.IsMine) return;

            _healthSlider.value = health;
            if (health <= 0)
            {
                _photonView.RPC("GetKilled", RpcTarget.AllViaServer);
            }
        }

        public void GetDamage(int damage)
        {
            _cameraShake.ShakeCameraOnce(1.7f);
            if (_photonView.IsMine)
            {
                _health -= damage;
            }
            ChangeHealth(_health);
        }

        protected override void GetKilled()
        {
            if (!_photonView.IsMine) return;

            base.GetKilled();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!_photonView.IsMine) return;

            if (!other.CompareTag("Bullet")) return;
            
            if(Equals(_photonView.Owner, other.GetComponent<PhotonView>().Owner)) return;
            
            GetDamage(10);
            other.gameObject.SetActive(false);
            other.GetComponent<Bullet>().Explode();
        }

        private void OnEnable()
        {
            //To override parent method
        }

        private void OnDisable()
        {
            //To override parent method
        }
    }
}