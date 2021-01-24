using UnityEngine;

namespace PlayerOfflineScipts
{
    public class Player : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private SoundPlayer _audioManager;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private Pooler _pooler;

        [Header("Objects")]
        [SerializeField] protected GameObject _playerExplosion;

        [TagSelector] 
        [SerializeField] protected string _lava;

        #region Event Subscription
        
        private void OnEnable()
        {
            _playerHealth.OnPlayerDefeated += Killed;
            _playerHealth.OnDamaged += PlayDamageSound;
        }

        private void OnDisable()
        {
            _playerHealth.OnPlayerDefeated += Killed;
            _playerHealth.OnDamaged -= PlayDamageSound;
        }
        
        #endregion

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
            _cameraShake.ShakeCameraOnce(2.2f);
            
            gameObject.SetActive(false);
        }
    }
}