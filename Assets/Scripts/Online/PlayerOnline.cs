using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using Cinemachine;

public class PlayerOnline : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject _playerExplosion;

    [SerializeField] private PhotonView _photonView;

    private float _health;
    
    private Slider _healthSlider;

    private float _remainingLives;

    private GameObject _endPanel;

    private Text _endPanelText;

    private Text _livesAmountText;


    //Components to DeActivate
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject fuelParticles;
    private PlayerMovementOnline _playerMovementOnline;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;


    private Vector2 deathPosition;
    private bool respawned, killed;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        if (!_photonView.IsMine) { return; }

        _healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();
        _endPanel = GameObject.Find("Canvas/EndPanel");
        _endPanelText = GameObject.Find("Canvas/EndPanel/Text").GetComponent<Text>();

        if(PhotonNetwork.IsMasterClient)
        {
            _livesAmountText = GameObject.Find("CanvasPlayer0Lives").GetComponent<Text>();
        }
        else
        {
            _livesAmountText = GameObject.Find("CanvasPlayer1Lives").GetComponent<Text>();
        }

        _playerMovementOnline = GetComponent<PlayerMovementOnline>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<CinemachineVirtualCamera>();        
    }

    private void Start()
    {
        if (!_photonView.IsMine) { return; }

        _endPanel.SetActive(false);

        _health = 100;
        _healthSlider.maxValue = _health;

        _remainingLives = 3;
        _livesAmountText.text = _remainingLives.ToString();

        killed = false;

        _cinemachineVirtualCamera.Follow = gameObject.transform;
        _cinemachineVirtualCamera.LookAt = gameObject.transform;
    }

    private void Update()
    {
        if (!_photonView.IsMine) { return; }

        CheckHealth();

        if(killed)
        {
            SetDeathPosition();
        }
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
        Debug.Log("DEAD");
        gameObject.transform.position = deathPosition;
    }
    #endregion

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
        if(killed) { return; }
                   
        PhotonNetwork.Instantiate(_playerExplosion.name, transform.position, Quaternion.identity);

        _remainingLives -= 1;
        _livesAmountText.text = _remainingLives.ToString();
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
        else
        {            
            _endPanel.SetActive(true);
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

    //
    public void Defeat()
    {

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
