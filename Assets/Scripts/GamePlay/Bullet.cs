using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _explosionName;
    [SerializeField] private float _speed = 0;
    
    private IDamageable _damageable;
    private Pooler _pooler;
    
    public void Init(Pooler pooler)
    {
        _pooler = pooler;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _damageable = other.GetComponent<IDamageable>();
        if(_damageable != null)
        {
            _damageable.ApplyDamage(10);
        }        
        Explode();
    }

    private void Explode()
    {
        _pooler.GetPooledObject(_explosionName.name, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
