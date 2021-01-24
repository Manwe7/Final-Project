using System;
using Interfaces;
using Managers;
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

        private void Start()
        {
            SetLifeProperties();
        }

        #region Event Subscription

        private void OnEnable()
        {
            _adsManager.OnAdWatched += SetLifeProperties;
        }

        private void OnDestroy()
        {
            _adsManager.OnAdWatched -= SetLifeProperties;
        }
        
        #endregion

        protected virtual void SetLifeProperties()
        {
            _health = 100;
            _healthSlider.maxValue = _health;
            ChangeHealth(_health);
            
            gameObject.SetActive(true);
        }

        protected virtual void ChangeHealth(float health)
        {        
            _healthSlider.value = health;
            if(health <= 0)
            {
                GetKilled();
            }
        }

        protected virtual void GetKilled()
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
            _cameraShake.ShakeCameraOnce(2.2f);
        }

        public event Action OnPlayerDefeated;
        public event Action OnDamaged;
    }
}