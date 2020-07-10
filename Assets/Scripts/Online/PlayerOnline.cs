using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using Cinemachine;

public class PlayerOnline : MonoBehaviour, IPunObservable
{
    private float _health, _fade = 0f;
    private PhotonView _photonView;

    private Slider _healthSlider = null;
    private Material _material = null;
    private bool _isFading;

    //Components to DeActivate
    [SerializeField] private GameObject weapon = null;
    private PlayerMovementOnline _playerMovementOnline;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    private bool respawned, killed;

    CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        _healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();
        _material = GetComponent<SpriteRenderer>().material;
        _photonView = GetComponent<PhotonView>();

        _playerMovementOnline = GetComponent<PlayerMovementOnline>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<CinemachineVirtualCamera>();

        if (_photonView.IsMine)
        {
            _cinemachineVirtualCamera.Follow = gameObject.transform;
            _cinemachineVirtualCamera.LookAt = gameObject.transform;
        }
    }

    private void OnEnable()
    {
        if (_photonView.IsMine)
        {
            _health = 100;
            _healthSlider.maxValue = _health;

            _isFading = true;
            killed = false;
        }
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            //_health
            _healthSlider.value = _health;
            if (_health <= 0 && !killed)
            {
                _photonView.RPC("Killed", RpcTarget.AllViaServer);
            }

            //_fade
            if (_isFading == true)
            {
                _fade += Time.deltaTime / 2;

                if (_fade >= 1)
                {
                    _fade = 1;
                    _isFading = false;
                }

                _material.SetFloat("__fade", _fade);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If touched lava - DIE
        if (other.gameObject.CompareTag("Lava"))
        {
            //Killed();
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
        /*if (_photonView.IsMine)
        {
            
        }*/

        killed = true;

        weapon.SetActive(false);
        _playerMovementOnline.enabled = false;
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

        //Some shake
        //CameraShake.ShakeOnce = true;
        _health = 0;
        _healthSlider.value = _health;
        respawned = false;

        StartCoroutine("WaitForRespawn");
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
            gameObject.transform.position = new Vector3(0, 0, 0);

            weapon.SetActive(true);
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
