using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerHealth : PlayerOfflineScipts.PlayerHealth, IPunObservable
    {
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private PlayerOnlineScripts.Player _playerOnline;

        private void Awake()
        {
            if (!_photonView.IsMine) { return; }

            _healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();

            _playerOnline.OnRespawn += Start;
        }

        private void Start()
        {
            if (!_photonView.IsMine) { return; }

            _health = 100;
            _healthSlider.maxValue = _health;
        }

        private void OnDestroy()
        {
            _playerOnline.OnRespawn -= Start;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            ChangeHealth();
        }

        private void ChangeHealth()
        {
            if (!_photonView.IsMine) { return; }

            _healthSlider.value = _health;
            if (_health <= 0 && !_playerOnline.killed) //Create event for killed
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
        }

        public override void GetKilled()
        {
            if (!_photonView.IsMine) { return; }

            base.GetKilled();
        }

        //Server
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_health);
            }
            else
            {
                _health = (int)stream.ReceiveNext();
            }
        }

        //public event Action OnPlayerDefeated;
    }
}