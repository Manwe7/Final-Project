using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private GameObject _explosionName;
    [SerializeField] private float _speed = 0;    
        
    private AudioManager _audioManager;
    
    private Pooler _pooler;
    
    private void Awake()
    {
        _pooler = Pooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            CameraShake.ShakeOnce = true;
            damageable.ApplyDamage(10);
        }        
        Explode();
    }

    private void Explode()
    {
        //_audioManager.Play(Sound.SoundNames.);
        _pooler.GetPooledObject(_explosionName.name, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
