using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _health, _fade = 0f;

    private Slider _healthSlider = null;           
    private Material _material = null;
    private bool _isFading;

    public delegate void Defeat();
    public static event Defeat defeated;

    //Object Pooler
    Pooler pooler;

    private void Awake()
    {
        _healthSlider = GameObject.Find("Canvas/PlayerHealthSlider").GetComponent<Slider>();
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        _health = 100;
        _healthSlider.maxValue = _health;
        
        _isFading = true;

        pooler = Pooler.Instance;
    }

    private void Update()
    {
        //_health
        _healthSlider.value = _health;
        if (_health <= 0)
        {
            Killed();
        }

        //_fade
        if (_isFading == true)
        {
            _fade += Time.deltaTime/2;

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
        _healthSlider.value = _health;
        //Turn off player
        gameObject.SetActive(false);
    }
}
