using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _explosionName;
    [SerializeField] private float _speed;

    [TagSelector]
    [SerializeField] private string _tagToAvoid; 
    
    private IDamageable _damageable;
    private Pooler _pooler;
    
    public void Awake()
    {    
        _pooler = FindObjectOfType<Pooler>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.GetComponent<Bullet>() != null || other.CompareTag(_tagToAvoid)) { return; }
        
        _damageable = other.GetComponent<IDamageable>();
        _damageable?.ApplyDamage(10);
        Explode();
    }

    private void Explode()
    {
        _pooler.GetPooledObject(_explosionName.name, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
