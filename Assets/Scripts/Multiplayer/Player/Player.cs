using UnityEngine;
using Photon.Pun;
using System.Collections;
using Cinemachine;
using System;

namespace PlayerOnlineScripts
{
    public class Player : MonoBehaviourPun
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private PlayerHealth _playerHealthOnline;
        [SerializeField] private PlayerLivesOnlineSync _playerLivesOnlineSync;

        [Header("Objects")]
        [SerializeField] protected GameObject _playerExplosion;

        [TagSelector] 
        [SerializeField] protected string _lava;

        #region Components to DeActivate
        [Header("Components to DeActivate")]
        [SerializeField] private GameObject _weapon;
        [SerializeField] private GameObject _fuelParticles;
        [SerializeField] private PlayerMovement _playerMovementOnline;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _blackSprite;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        #endregion

        private Vector2 _deathPosition;
        private bool _respawned;
        private bool _killed;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private void Awake()
        {
            if (!_photonView.IsMine) { return; }

            _cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<CinemachineVirtualCamera>();
            
            _playerHealthOnline.OnPlayerDefeated += KillPlayer;
        }

        private void Start()
        {
            if (!_photonView.IsMine) { return; }

            _killed = false;

            _cinemachineVirtualCamera.Follow = gameObject.transform;
            _cinemachineVirtualCamera.LookAt = gameObject.transform;
        }

        private void OnDestroy()
        {
            _playerHealthOnline.OnPlayerDefeated -= KillPlayer;
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            if(_killed)
            {
                SetDeathPosition();
            }            
        }

        private void SetDeathPosition()
        {        
            gameObject.transform.position = _deathPosition;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag(_lava)) return;
            
            if (!_photonView.IsMine) return;
                
            _playerHealthOnline.GetDamage(1000);
            _photonView.RPC("GetKilled", RpcTarget.AllViaServer);
        }

        private void KillPlayer()
        {
            if(!_photonView.IsMine) return;
            
            _photonView.RPC("GetKilled", RpcTarget.AllViaServer);
        }

        [PunRPC]
        public void GetKilled()
        {
            if(_killed) { return; }

            _killed = true;
            
            PhotonNetwork.Instantiate(_playerExplosion.name, transform.position, Quaternion.identity);

            OnDefeat?.Invoke();

            _deathPosition = gameObject.transform.position;

            SetComponentsState(false, RigidbodyType2D.Kinematic);
            
            _respawned = false;
            
            if(_playerLivesOnlineSync.IsEnoughLives())
            {
                StartCoroutine(WaitForRespawn());
            }
        }

        private IEnumerator WaitForRespawn()
        {
            yield return new WaitForSeconds(3f);

            if (_photonView.IsMine)
            {
                _photonView.RPC("RespawnPlayer", RpcTarget.AllViaServer);
            }
        }

        [PunRPC]
        public void RespawnPlayer()
        {
            if (!_respawned)
            {
                _killed = false;

                var pos = GameObject.Find("Managers").GetComponent<OnlineManager>().ChoosePos();
                gameObject.transform.position = pos;
                
                SetComponentsState(true, RigidbodyType2D.Dynamic);

                OnRespawn?.Invoke();

                _respawned = true;
            }
        }

        [PunRPC]
        private void SetComponentsState(bool state, RigidbodyType2D type)
        {
            _weapon.SetActive(state);
            _fuelParticles.SetActive(state);
            _spriteRenderer.enabled = state;
            _blackSprite.enabled = state;
            _boxCollider2D.enabled = state;
            _playerMovementOnline.enabled = state;
            _rigidbody2D.bodyType = type;
        }

        public event Action OnRespawn;

        public event Action OnDefeat;
    }
}