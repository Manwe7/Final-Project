using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Mirror;

public class PlayerOnline : NetworkBehaviour
{
    [SerializeField] private GameObject BulletPrefab = null;
    [SerializeField] private Transform barrel = null;

    private Slider healthSlider = null;

    [SyncVar]
    [HideInInspector]
    public float _health;

    private float _fade = 0f;
    private Material _material = null;
    private bool _isFading;

    /*public delegate void Defeat();
    public static event Defeat defeated;*/

    private void OnEnable()
    {
        PlayerOnlineWeapon.Shoot += CmdFire;
    }

    private void OnDisable()
    {
        PlayerOnlineWeapon.Shoot -= CmdFire;
    }

    /*var vcam = GameObject.Find("Main Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>();
    vcam.LookAt = gameObject.transform;
    vcam.Follow = gameObject.transform;*/

    private void Start()
    {
        healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();

        _health = 100;
        healthSlider.maxValue = _health;

        _material = GetComponent<SpriteRenderer>().material;
        _isFading = true;
    }

    private void Update()
    {
        //_health
        healthSlider.value = _health;
        if (_health <= 0)
        {
            Killed();
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

    // this is called on the server
    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(BulletPrefab, barrel.position, barrel.rotation);
        bullet.name = gameObject.name + "Bullet";
        NetworkServer.Spawn(bullet);        
    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D other)
    {
        //If touched lava - DIE
        if (other.gameObject.CompareTag("Lava"))
        {
            Killed();
        }
    }

    [ServerCallback]
    void GetDamage(int damage)
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("Hurt");
        _health -= damage;
    }

    [Server]
    void Killed()
    {
        _health = 0;
        NetworkServer.Destroy(gameObject);
        gameObject.SetActive(false);
        /*defeated?.Invoke();
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        //Some particles
        //Pooler
        GameObject explosion = pooler.GetPooledObject("PlayerExplosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.SetActive(true); //end

        //Some shake
        CameraShake.ShakeOnce = true;
        //_health is 0
        _health = 0;
        healthSlider.value = _health;
        //Turn off player
        gameObject.SetActive(false);*/
    }
}
