using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Mirror;

public class PlayerOnline : MonoBehaviour
{
    private Slider healthSlider = null;

    private float _health, _fade = 0f;
    private Material _material = null;
    private bool _isFading;

    public delegate void Defeat();
    public static event Defeat defeated;

    //Object Pooler
    Pooler pooler;

    private void OnEnable()
    {
        /*var vcam = GameObject.Find("Main Camera/CM vcam1").GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = gameObject.transform;
        vcam.Follow = gameObject.transform;*/
    }

    private void Start()
    {
        healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();

        _health = 100;
        healthSlider.maxValue = _health;

        _material = GetComponent<SpriteRenderer>().material;
        _isFading = true;

        pooler = Pooler.Instance;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If touched lava - DIE
        if (other.gameObject.CompareTag("Lava"))
        {
            Killed();
        }
    }

    void GetDamage(float damage)
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("Hurt");
        _health -= damage;
    }

    void Killed()
    {
        if (defeated != null)
        { defeated(); }
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
        gameObject.SetActive(false);
    }
}
