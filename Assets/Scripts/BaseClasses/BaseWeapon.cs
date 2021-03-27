using System;
using System.Collections;
using UnityEngine;

namespace BaseClasses
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected GameObject _bullet;
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected Pooler _pooler;

        protected float _reloadTime;
        protected bool _isReloaded;

        protected float _minReloadTime;
        protected float _maxReloadTime;

        protected event Action OnShoot;

        protected void Shoot()
        {
            if (!_isReloaded) return;
        
            _pooler.GetPooledObject(_bullet.name, _barrel.position, _barrel.rotation);
            StartCoroutine(Reload());
            OnShoot?.Invoke();
        }

        protected IEnumerator Reload()
        {
            _isReloaded = false;
            yield return new WaitForSeconds(_reloadTime);
            _isReloaded = true;
        }
    }
}
