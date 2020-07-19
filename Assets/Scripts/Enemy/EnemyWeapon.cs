using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{    
    [SerializeField] private Transform _barrel = null;    
    [SerializeField] private GameObject _bullet = null;

    [Header("Min and Max realod time")]
    [SerializeField] private int _minReloadTime;
    [SerializeField] private int _maxReloadTime;

    private GameObject _player;
    private int _reloadTime;
    private bool _reloaded;
    private Pooler _pooler;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pooler = Pooler.Instance;
    }

    private void OnEnable()
    {
        StartCoroutine(Reload());
    }

    private void Update()
    {
        if(_player != null)
        {
            Shoote();
        }
    }

    private void Shoote()
    {
        if (_reloaded)
        {
            _pooler.GetPooledObject(_bullet.name, _barrel.position, _barrel.rotation);
            
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