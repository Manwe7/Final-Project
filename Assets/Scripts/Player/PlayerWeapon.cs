using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{   
    [SerializeField] private GameObject _playerBullet;
    [SerializeField] private Transform _barrel;
    [SerializeField] private float _reloadTime = 0;

    private bool _reloaded;
    private Pooler _pooler;

    private void Start()
    {
        _reloaded = true;
        _pooler = Pooler.Instance;
    }

    private void Update()
    {        
        Shoote();
    }

    private void Shoote()
    {
        if (_reloaded)
        {
            _pooler.GetPooledObject(_playerBullet.name, _barrel.position, _barrel.rotation);            

            _reloaded = false;
            Invoke("Reload", _reloadTime);
        }
    }

    private void Reload()
    {
        _reloaded = true;
    }
}
