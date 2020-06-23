using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    //Object Pooler
    Pooler pooler;

    private void Start()
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerBullet");

        pooler = Pooler.Instance;
    }

    private void FixedUpdate()
    {
        //Constant speed
        _rigidbody2D.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
        {
            Explode();
        }
        //If player send damage
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessageUpwards("GetDamage", 10);
            CameraShake.ShakeOnce = true;
            
            Explode();
        }
        //If enemy send damage
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessageUpwards("GetDamage", 10);
            CameraShake.ShakeOnce = true;

            Explode();
        }
    }

    void Explode()
    {
        //tones of if by name....
        //1 - better create a property (type Enum) with predefined values
        //2 - even better solution - use strategy pattern and inject proper explosion type whe you initialize the object
        //3 - the logic in if is 75% the same. the difference is only which object you got from pool, this requires refactoring
        //Depending on bullet instantiate corresponding particles
        if(gameObject.name == "PlayerBullet")
        {
            GameObject explosion = pooler.GetPooledObject("PlayerBulletExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true);
        }
        else if(gameObject.name == "LittleEnemyBullet")
        {
            GameObject explosion = pooler.GetPooledObject("LittleEnemyBulletExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true);
        }
        else if(gameObject.name == "AverageEnemyBullet")
        {
            GameObject explosion = pooler.GetPooledObject("AverageEnemyBulletExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true);
        }
        else if(gameObject.name == "MegaEnemyBullet")
        {
            GameObject explosion = pooler.GetPooledObject("MegaEnemyBulletExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
