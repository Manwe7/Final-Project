using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerOfflineScipts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] protected Slider _healthSlider;

        [SerializeField] protected CameraShake _cameraShake;

        [SerializeField] private AdsManager _adsManager;

        public int _health;

        private void Awake()
        {
            _adsManager.OnAdWatched += SetLifeProperties;
        }

        private void Start()
        {
            SetLifeProperties();
        }

        private void OnDestroy()
        {
            _adsManager.OnAdWatched -= SetLifeProperties;
        }

        public virtual void SetLifeProperties()
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
                GetKilled();
            }
        }

        public virtual void GetKilled()
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

        private void ShakeCamera()
        {
            _cameraShake.ShakeCameraOnce();
        }

        public event Action OnPlayerDefeated;
        public event Action OnDamaged;
    }
}