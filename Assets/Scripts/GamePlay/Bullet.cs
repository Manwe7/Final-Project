using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    private string _explosionName;

    //Object _pooler
    Pooler _pooler;

    //Audio Manager
    AudioManager _audioManager;

    private void Awake()
    {
        _pooler = Pooler.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        //Play sound
        _audioManager.Play("PlayerBullet");

        _explosionName = gameObject.name + "Explosion";        
    }

    private void FixedUpdate()
    {
        //Constant speed
        _rigidbody2D.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Lava"))
        {
            Explode();
        }
        //If player send damage
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetDamage(10);
            CameraShake.ShakeOnce = true;
            
            Explode();
        }
        //If enemy send damage
        if(other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(10);
            CameraShake.ShakeOnce = true;

            Explode();
        }
    }

    void Explode()
    {
        GameObject explosion = _pooler.GetPooledObject(_explosionName, transform.position, Quaternion.identity);        

        gameObject.SetActive(false);
    }
}
