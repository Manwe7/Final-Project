using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider _healthSlider;
    private int _health;

    public event Action IsKilled;

    private void Start()
    {
        _health = 100;
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _healthSlider.value = _health;
    } 

    public void Killed()
    {        
        _health = 0;
        _healthSlider.value = _health;
        IsKilled();
    }    
}
