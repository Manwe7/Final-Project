using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{   
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Pooler _pooler;
    
    [Header("Objects")]
    [SerializeField] private GameObject _playerBullet;
    [SerializeField] private Transform _barrel;

    [SerializeField] private float _reloadTime;        

    private bool _reloaded;    

    private void Start()
    {
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
            _audioManager.Play(SoundNames.PlayerBullet);
            var bullet = _pooler.GetPooledObject(_playerBullet.name, _barrel.position, _barrel.rotation).GetComponent<Bullet>();
            bullet.Init(_pooler);

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
