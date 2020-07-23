using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] CameraShake _cameraShake;
    private int _health;

    private void Start()
    {
        _health = 100;
        ChangeHealth(_health);
    }

    private void ChangeHealth(float health)
    {
        OnHealthChanged?.Invoke(health);
        if(health <= 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        OnPlayerDefeated?.Invoke();
        _health = 0;
    }

    public void ApplyDamage(int damage)
    {
        ChangeHealth(_health -= damage);
        ShakeCamera();
    }

    public void ShakeCamera()
    {
        _cameraShake.ShakeCameraOnce();
    }

    public event Action OnPlayerDefeated;
    public event Action<float> OnHealthChanged;
}
