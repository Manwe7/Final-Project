using BaseClasses;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Weapon
{
    public class EnemyWeapon : BaseWeapon
    {
        [SerializeField] private EnemyWeaponSettings _enemyWeaponSettings;
        [SerializeField] private BaseEnemy _baseEnemy;

        private GameObject _player;

        public void Init(GameObject player, Pooler pooler)
        {
            _player = player;
            _pooler = pooler;
        }

        private void Awake()
        {
            _minReloadTime = _enemyWeaponSettings._minReloadTime;
            _maxReloadTime = _enemyWeaponSettings._maxReloadTime;
            
            SetRandomReloadTime();
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
            if(_player == null || !_baseEnemy.IsPlayerInXRange() || !_baseEnemy.IsPlayerInYRange()) { return; }
            
            Shoot();
        }

        private void SetRandomReloadTime()
        {
            _reloadTime = Random.Range(_minReloadTime, _maxReloadTime);
        }
    }
}