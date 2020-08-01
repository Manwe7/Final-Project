using UnityEngine;

public class EnemyWeapon : BaseWeapon
{    
    [Header("Min and Max realod time")]
    [SerializeField] private int _minReloadTime;

    [SerializeField] private int _maxReloadTime;

    private GameObject _player;    

    private Pooler _pooler;

    public void Init(GameObject player, Pooler pooler)
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
            _pooler.GetPooledObject(_bullet.name, _barrel.position, _barrel.rotation);
            
            StartCoroutine(Reload());

            _reloadTime = Random.Range(_minReloadTime, _maxReloadTime);
        }
    }
}