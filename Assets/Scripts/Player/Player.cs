using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _health, _fade = 0f;
    private bool _isFading;

    private AudioManager _audioManager;
    private Slider _healthSlider;
    private Material _material;    

    public delegate void Defeat();
    public static event Defeat defeated;

    //Object Pooler
    Pooler pooler;

    private void Awake()
    {
        _healthSlider = GameObject.Find("CanvasUI/PlayerHealthSlider").GetComponent<Slider>();
        _material = GetComponent<SpriteRenderer>().material;
        _audioManager = FindObjectOfType<AudioManager>();
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
        MaterialAnimation();
    }

    private void MaterialAnimation()
    {
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

    public void GetDamage(float damage)
    {
        //Play sound
        _audioManager.Play("Hurt");
        _health -= damage;

        //Show changed values
        _healthSlider.value = _health;
        if (_health <= 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        defeated();

        //Play sound
        _audioManager.Play("PlayerDeath");
        
        //Pooler
        pooler.GetPooledObject("PlayerExplosion", transform.position, Quaternion.identity);        

        //Some shake
        CameraShake.ShakeOnce = true;
                
        _health = 0;
        _healthSlider.value = _health;
        
        //Turn off player
        gameObject.SetActive(false);
    }
}
