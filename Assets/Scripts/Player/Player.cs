using UnityEngine;
using System;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{    
    [SerializeField] private GameObject _playerExplosion;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Pooler _pooler;

    private PlayerHealth _playerHealth;
    
    public event Action OnPlayerDefeated;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();

        _playerHealth.IsKilled += Killed;
    }

    private void OnDisable()
    {
        _playerHealth.IsKilled += Killed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            Killed();
        }
    }

    public void GetDamage(int damage) //ApplyDamage
    {
        _audioManager.Play("Hurt");

        _playerHealth.ApplyDamage(damage);
    }
    
    private void Killed()
    {
        OnPlayerDefeated();
    
        _audioManager.Play("PlayerDeath");
        
        _pooler.GetPooledObject(_playerExplosion.name, transform.position, Quaternion.identity);

        CameraShake.ShakeOnce = true;                        
        
        gameObject.SetActive(false);
    }
}
