using System.Collections;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected GameObject _bullet;

    [SerializeField] protected Transform _barrel;

    protected float _reloadTime;

    protected bool _reloaded;

    protected IEnumerator Reload()
    {
        _reloaded = false;
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
