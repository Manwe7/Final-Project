using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{    
    [SerializeField] private Transform _barrel;
    [SerializeField] private GameObject _bullet;
    
    [Header("Min and Max realod time")]
    [SerializeField] private int _minReloadTime;
    [SerializeField] private int _maxReloadTime;

    private GameObject _player;
    private int _reloadTime;
    private bool _reloaded;
    private Pooler _pooler;

    public void Init(GameObject player, AudioManager audioManager, Pooler pooler)
    {
        _player = player;
        _pooler = pooler;
    }

    private void OnEnable()
    {
        StartCoroutine(Reload());
    }

    private void Update()
    {
        if(_player != null)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_reloaded)
        {
            var bullet = _pooler.GetPooledObject(_bullet.name, _barrel.position, _barrel.rotation).GetComponent<Bullet>();
            bullet.Init(_pooler);

            StartCoroutine(Reload());
        }
    }

    protected IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(_minReloadTime, _maxReloadTime);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }    
}