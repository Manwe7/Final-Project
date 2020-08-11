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
        [SerializeField] private GameObject weapon;

        [SerializeField] private GameObject fuelParticles;

        [SerializeField] private PlayerMovement _playerMovementOnline;

        [SerializeField] private SpriteRenderer _spriteRenderer;

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
            
            _playerHealthOnline.OnPlayerDefeated += GetKilled;
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
            _playerHealthOnline.OnPlayerDefeated -= GetKilled;
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
            if (other.gameObject.CompareTag(_lava))
            {
                _playerHealthOnline.GetDamage(1000);
                _photonView.RPC("GetKilled", RpcTarget.AllViaServer);
            }
        }

        [PunRPC]
        public void GetKilled()
        {
            if(!_photonView.IsMine) { return; }

            if(_killed) { return; }

            PhotonNetwork.Instantiate(_playerExplosion.name, transform.position, Quaternion.identity);

            _playerLivesOnlineSync.DecreaseOneLife();

            _killed = true;

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

            _photonView.RPC("RespawnPlayer", RpcTarget.AllViaServer);
        }

        [PunRPC]
        public void RespawnPlayer()
        {
            if(!_photonView.IsMine) { return; }
            
            if (!_respawned)
            {
                _killed = false;

                int posX = UnityEngine.Random.Range(-55, 55);
                int posy = UnityEngine.Random.Range(-7, 25);
                gameObject.transform.position = new Vector3(posX, posy, 0);                
                
                SetComponentsState(true, RigidbodyType2D.Dynamic);

                OnRespawn?.Invoke();

                _respawned = true;
            }
        }

        [PunRPC]
        private void SetComponentsState(bool state, RigidbodyType2D type)
        {
            weapon.SetActive(state);
            fuelParticles.SetActive(state);
            _spriteRenderer.enabled = state;
            _boxCollider2D.enabled = state;
            _playerMovementOnline.enabled = state;
            _rigidbody2D.bodyType = type;
        }

        public event Action OnRespawn;
    }
}