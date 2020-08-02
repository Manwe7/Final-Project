using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

namespace PlayerOnlineScripts
{
    public class PlayerHealth : MonoBehaviour, IPunObservable
    {
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private PlayerOnlineScripts.Player _playerOnline;

        public float _health;
        
        private Slider _healthSlider;

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

            CheckHealth();
        }

        private void CheckHealth()
        {
            if (!_photonView.IsMine) { return; }

            _healthSlider.value = _health;
            if (_health <= 0 && !_playerOnline.killed)
            {
                _photonView.RPC("Killed", RpcTarget.AllViaServer);
            }
        }

        public void GetDamage(float damage)
        {
            if (_photonView.IsMine)
            {
                _health -= damage;
            }
        }

        private void Killed()
        {
            if (!_photonView.IsMine) { return; }
            
            _health = 0;
            _healthSlider.value = _health;
            OnPlayerDefeated?.Invoke();
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
                _health = (float)stream.ReceiveNext();
            }
        }

        public event Action OnPlayerDefeated;
    }
}