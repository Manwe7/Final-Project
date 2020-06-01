using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider healthSlider = null;    
    
    private float _health, _fade = 0f;
    private Material _material = null;
    private bool _isFading;

    //Object Pooler
    ExplosionPooler explosionPooler;

    private void Start()
    {
        _health = 100;
        healthSlider.maxValue = _health;

        _material = GetComponent<SpriteRenderer>().material;
        _isFading = true;

        explosionPooler = ExplosionPooler._instance;
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
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        //Some particles
        explosionPooler.SpawnFromPool("PlayerExplosion", transform.position, transform.rotation);
        //Some shake
        CameraShake.ShakeOnce = true;
        //_health is 0
        _health = 0;
        healthSlider.value = _health;
        //Turn off player
        gameObject.SetActive(false);
    }
}
