using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] Rigidbody2D rb = null;

    //Object Pooler
    ObjectPooler objectPooler;

    private void Start()
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerBullet");

        objectPooler = ObjectPooler.objectPoolerInstance;
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
        if(other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SendMessageUpwards("Explode");
            Explode();
        }
    }

    void Explode()
    {
        //Depending on bullet instantiate corresponding particles
        if(gameObject.name == "PlayerBullet(Clone)")
        {            
            objectPooler.SpawnFromPool("PlayerBulletExplosion", transform.position, Quaternion.identity);
        }
        else if(gameObject.name == "EnemyBullet(Clone)")
        {         
            objectPooler.SpawnFromPool("LittleEnemyBulletExplosion", transform.position, Quaternion.identity);
        }
        else if(gameObject.name == "LittleEnemyBullet(Clone)")
        {         
            objectPooler.SpawnFromPool("AverageEnemyBulletExplosion", transform.position, Quaternion.identity);
        }
        else if(gameObject.name == "MegaEnemyBullet(Clone)")
        {         
            objectPooler.SpawnFromPool("MegaEnemyBulletExplosion", transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
