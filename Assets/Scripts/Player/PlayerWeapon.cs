using UnityEngine;

namespace PlayerOfflineScipts
{
    public class PlayerWeapon : BaseWeapon
    {
        [Header("Scripts")]
        [SerializeField] private SoundPlayer _soundPlayer;

        [SerializeField] private Pooler _pooler;
        
        [Header("Objects")]
        [SerializeField] private GameObject _playerBullet;

        private void Start()
        {
            _reloadTime = 0.3f;
            _reloaded = true;
        }

        private void Update()
        {        
            Shoot();
        }

        private void Shoot()
        {
            if (_reloaded)
            {
                _soundPlayer.Play(SoundNames.PlayerBullet);
                _pooler.GetPooledObject(_playerBullet.name, _barrel.position, _barrel.rotation);            
                
                StartCoroutine(Reload());
            }
        }
    }
}