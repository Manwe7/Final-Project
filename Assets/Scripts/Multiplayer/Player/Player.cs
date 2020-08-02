using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using Cinemachine;
using System;

namespace PlayerOnlineScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private PlayerHealth _playerHealthOnline;
        
        [SerializeField] private GameObject _playerExplosion;    

        private int _remainingLives;

        private GameObject _endPanel;

        private Text _endPanelText;

        [Header("Components to DeActivate")]
        [SerializeField] private GameObject weapon;

        [SerializeField] private GameObject fuelParticles;

        [SerializeField] private PlayerMovement _playerMovementOnline;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private BoxCollider2D _boxCollider2D;

        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Vector2 deathPosition;

        private bool respawned;

        public bool killed;

        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

        private void Awake()
        {
            if (!_photonView.IsMine) { return; }

            FindUIObjects();
            
            _playerHealthOnline.OnPlayerDefeated += Killed;
        }

        private void FindUIObjects()
        {
            _endPanel = GameObject.Find("Canvas/EndPanel");
            _endPanelText = GameObject.Find("Canvas/EndPanel/Text").GetComponent<Text>();

            _cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            if (!_photonView.IsMine) { return; }

            _endPanel.SetActive(false);        

            SetLives(3);

            killed = false;

            _cinemachineVirtualCamera.Follow = gameObject.transform;
            _cinemachineVirtualCamera.LookAt = gameObject.transform;
        }

        private void OnDestroy()
        {
            _playerHealthOnline.OnPlayerDefeated -= Killed;
        }

        private void SetLives(int value)
        {
            _remainingLives = value;
            _myCustomProperties[ShowScoreOnline.healthSave] = _remainingLives;        
            PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        }

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            if(killed)
            {
                SetDeathPosition();
            }

            EndGamePanel();
        }

        private void SetDeathPosition()
        {        
            gameObject.transform.position = deathPosition;
        }

        //[PunRPC]
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Lava"))
            {
                _playerHealthOnline.GetDamage(1000);
                _photonView.RPC("Killed", RpcTarget.AllViaServer);
            }
        }

        [PunRPC]
        public void Killed()
        {
            if(!_photonView.IsMine) { return; }

            if(killed) { return; }

            PhotonNetwork.Instantiate(_playerExplosion.name, transform.position, Quaternion.identity);

            _remainingLives -= 1;
            SetLives(_remainingLives);

            killed = true;
            deathPosition = gameObject.transform.position;

            weapon.SetActive(false);
            fuelParticles.SetActive(false);
            _playerMovementOnline.enabled = false;
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            
            respawned = false;
            
            if(_remainingLives > 0)
            {
                StartCoroutine(WaitForRespawn());
            }
        }

        private void EndGamePanel()
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(PhotonNetwork.PlayerList[i].CustomProperties[ShowScoreOnline.healthSave] != null)
                {
                    int lives = (int)PhotonNetwork.PlayerList[i].CustomProperties[ShowScoreOnline.healthSave];
                    if(lives <= 0)
                    {
                        StartCoroutine(StopGame());
                    }
                }
            }        
        }

        private IEnumerator StopGame()
        {
            yield return new WaitForSeconds(1.7f);
            Time.timeScale = 0.1f;
            _endPanel.SetActive(true);
            
            if(_remainingLives > 0)
            {
                _endPanelText.text = "VICTORY";    
            }
            else
            {
                _endPanelText.text = "DEFEAT";
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
            if (!respawned)
            {
                killed = false;

                int posX = UnityEngine.Random.Range(-55, 55);
                int posy = UnityEngine.Random.Range(-7, 25);
                gameObject.transform.position = new Vector3(posX, posy, 0);

                weapon.SetActive(true);
                fuelParticles.SetActive(true);
                _spriteRenderer.enabled = true;
                _boxCollider2D.enabled = true;
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody2D.gravityScale = 0;
                _playerMovementOnline.enabled = true;

                OnRespawn();

                respawned = true;
            }
        }

        public event Action OnRespawn;
    }
}