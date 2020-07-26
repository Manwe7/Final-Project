using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider _healthSlider;

    [SerializeField] CameraShake _cameraShake;

    private int _health;

    private void Start()
    {
        _health = 100;
        _healthSlider.maxValue = _health;
        ChangeHealth(_health);
    }

    private void ChangeHealth(float health)
    {        
        _healthSlider.value = health;
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
        OnDamaged?.Invoke();
        ChangeHealth(_health -= damage);
        ShakeCamera();
    }

    public void ShakeCamera()
    {
        _cameraShake.ShakeCameraOnce();
    }

    public event Action OnPlayerDefeated;
    public event Action OnDamaged;
}
