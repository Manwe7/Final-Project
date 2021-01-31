using System;
using System.Collections;
using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _explosionName;
    [SerializeField] private float _speed;

    [TagSelector]
    [SerializeField] private string _tagToAvoid;

    [Header("Components to disable")] 
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private IDamageable _damageable;
    private Pooler _pooler;
    
    public void Awake()
    {    
        _pooler = FindObjectOfType<Pooler>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
        _collider2D.enabled = true;
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
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;
        _pooler.GetPooledObject(_explosionName.name, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(0.55f);
        
        gameObject.SetActive(false);
    }
}
