using System;
using System.Collections;
using UnityEngine;

namespace BaseClasses
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected GameObject _bullet;
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected Pooler _pooler;

        protected float _reloadTime;
        protected bool _reloaded;

        protected event Action OnShoot;

        protected void Shoot()
        {
            if (!_reloaded) return;
        
            _pooler.GetPooledObject(_bullet.name, _barrel.position, _barrel.rotation);
            StartCoroutine(Reload());
            OnShoot?.Invoke();
        }

        protected IEnumerator Reload()
        {
            _reloaded = false;
            yield return new WaitForSeconds(_reloadTime);
            _reloaded = true;
        }
    }
}
