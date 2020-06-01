using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D rb = null;

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
        rb.velocity = transform.up * speed;
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
