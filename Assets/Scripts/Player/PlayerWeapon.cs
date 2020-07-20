using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{   
    [SerializeField] private GameObject _playerBullet;
    [SerializeField] private Transform _barrel;
    [SerializeField] private float _reloadTime = 0;

    [SerializeField] private AudioManager _audioManager;

    private bool _reloaded;
    private Pooler _pooler; // in inspector

    private void Start()
    {
        _reloaded = true;
        _pooler = Pooler.Instance;
    }

    private void Update()
    {        
        Shoot();
    }

    private void Shoot()
    {
        if (_reloaded)
        {
            _audioManager.Play(Sound.SoundNames.PlayerBullet);
            _pooler.GetPooledObject(_playerBullet.name, _barrel.position, _barrel.rotation);

            _reloaded = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {        
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
