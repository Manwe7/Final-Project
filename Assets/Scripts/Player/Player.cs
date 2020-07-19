using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;   
    [SerializeField] private GameObject _playerExplosion;

    private AudioManager _audioManager;
    private Pooler _pooler;
    private float _health;
    
    public event Action OnPlayerDefeated;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _health = 100;
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;

        _pooler = Pooler.Instance;
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
        _audioManager.Play("Hurt");

        _health -= damage;
        _healthSlider.value = _health;
        if (_health <= 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        OnPlayerDefeated();
    
        _audioManager.Play("PlayerDeath");
        
        _pooler.GetPooledObject(_playerExplosion.name, transform.position, Quaternion.identity);        

        CameraShake.ShakeOnce = true;
                
        _health = 0;
        _healthSlider.value = _health;
        
        gameObject.SetActive(false);
    }
}
