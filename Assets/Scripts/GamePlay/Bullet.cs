using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private float _speed = 0;
    
    private string _explosionName;

    private AudioManager _audioManager;
    private Pooler _pooler;
    
    private void Awake()
    {
        _pooler = Pooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {       
        _audioManager.Play("PlayerBullet");

        _explosionName = gameObject.name + "Explosion";
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Lava"))
        {
            Explode();
        }
        else if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetDamage(10);
            CameraShake.ShakeOnce = true;
            
            Explode();
        }
        else if(other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(10);
            CameraShake.ShakeOnce = true;

            Explode();
        }
    }

    private void Explode()
    {
        _pooler.GetPooledObject(_explosionName, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
