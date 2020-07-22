using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{    
    [Header("Objects")]
    [SerializeField] private GameObject _playerExplosion;
    
    [SerializeField] private Slider _healthSlider;

    [Header("Scipts")]
    [SerializeField] private AudioManager _audioManager;
    
    [SerializeField] private PlayerHealth _playerHealth;
    
    [SerializeField] private CameraShake _cameraShake;
    
    [SerializeField] private Pooler _pooler;

    [TagSelector] 
    [SerializeField] private string Lava = "";
    
    private void Awake()//where to set Slider max value?
    {        
        _playerHealth.OnPlayerDefeated += Killed;
        _playerHealth.OnHealthChanged += ChangeHealthSliderValue;
    }

    private void OnDisable()
    {
        _playerHealth.OnPlayerDefeated += Killed;
        _playerHealth.OnHealthChanged -= ChangeHealthSliderValue;
    }

    private void PlayDamageSound()
    {
        _audioManager.Play(SoundNames.Hurt);
    }

    private void ChangeHealthSliderValue(float health)
    {        
        _healthSlider.value = health;
        PlayDamageSound();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lava")) // create const string
        {
            _playerHealth.ApplyDamage(10000);
        }
    }

    private void Killed()
    {    
        _audioManager.Play(SoundNames.PlayerDeath);
        
        _pooler.GetPooledObject(_playerExplosion.name, transform.position, Quaternion.identity);

        _cameraShake.ShakeCameraOnce();
        
        gameObject.SetActive(false);
    }
}
