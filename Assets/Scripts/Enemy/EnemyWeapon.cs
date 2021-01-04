using ScriptableObjects;
using UnityEngine;

public class EnemyWeapon : BaseWeapon
{
    [SerializeField] private EnemyWeaponSettings _enemyWeaponSettings;

    private GameObject _player;

    public void Init(GameObject player, Pooler pooler)
    {
        _player = player;
        _pooler = pooler;
    }

    private void OnEnable()
    {
        StartCoroutine(Reload());

        OnShoot += SetRandomReloadTime;
    }

    private void OnDisable()
    {
        OnShoot -= SetRandomReloadTime;
    }

    private void Update()
    {
        if(_player != null)
        {
            Shoot();
        }
    }

    private void SetRandomReloadTime()
    {
        _reloadTime = Random.Range(_enemyWeaponSettings._minReloadTime, _enemyWeaponSettings._maxReloadTime);
    }
}