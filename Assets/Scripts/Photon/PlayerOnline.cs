using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using Cinemachine;

public class PlayerOnline : MonoBehaviour, IPunObservable
{
    [SerializeField] private PhotonView _photonView;
    
    [SerializeField] private GameObject _playerExplosion;    

    private float _health;
    
    private Slider _healthSlider;

    private int _remainingLives;

    private GameObject _endPanel;

    private Text _endPanelText;

    [Header("Components to DeActivate")]
    [SerializeField] private GameObject weapon;

    [SerializeField] private GameObject fuelParticles;

    [SerializeField] private PlayerMovementOnline _playerMovementOnline;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private BoxCollider2D _boxCollider2D;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector2 deathPosition;

    private bool respawned, killed;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        if (!_photonView.IsMine) { return; }

        _healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();
        _endPanel = GameObject.Find("Canvas/EndPanel");
        _endPanelText = GameObject.Find("Canvas/EndPanel/Text").GetComponent<Text>();

        _cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<CinemachineVirtualCamera>();        
    }

    private void Start()
    {
        if (!_photonView.IsMine) { return; }

        _endPanel.SetActive(false);

        _health = 100;
        _healthSlider.maxValue = _health;

        SetLives(3);

        killed = false;

        _cinemachineVirtualCamera.Follow = gameObject.transform;
        _cinemachineVirtualCamera.LookAt = gameObject.transform;
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

        CheckHealth();

        if(killed)
        {
            SetDeathPosition();
        }

        EndGamePanel();
    }

    #region Update methods
    private void CheckHealth()
    {
        _healthSlider.value = _health;
        if (_health <= 0 && !killed)
        {
            _photonView.RPC("Killed", RpcTarget.AllViaServer);
        }
    }

    private void SetDeathPosition()
    {        
        gameObject.transform.position = deathPosition;
    }
    #endregion

    [PunRPC]
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
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

        _health = 0;
        _healthSlider.value = _health;
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

            int posX = Random.Range(-55, 55);
            int posy = Random.Range(-7, 25);
            gameObject.transform.position = new Vector3(posX, posy, 0);

            weapon.SetActive(true);
            fuelParticles.SetActive(true);
            _spriteRenderer.enabled = true;
            _boxCollider2D.enabled = true;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.gravityScale = 0;
            _playerMovementOnline.enabled = true;

            _health = 100;

            respawned = true;
        }
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
}
