using UnityEngine;

public class Player : MonoBehaviour
{    
    [Header("Objects")]
    [SerializeField] private GameObject _playerExplosion;
        

    [Header("Scipts")]
    [SerializeField] private AudioManager _audioManager;
    
    [SerializeField] private PlayerHealth _playerHealth;
    
    [SerializeField] private CameraShake _cameraShake;
    
    [SerializeField] private Pooler _pooler;

    [TagSelector] 
    [SerializeField] private string _lava;
    
    private void Awake()
    {        
        _playerHealth.OnPlayerDefeated += Killed;
        _playerHealth.OnDamaged += PlayDamageSound;
    }

    private void OnDisable()
    {
        _playerHealth.OnPlayerDefeated += Killed;
        _playerHealth.OnDamaged -= PlayDamageSound;
    }

    private void PlayDamageSound()
    {
        _audioManager.Play(SoundNames.Hurt);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_lava))
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
