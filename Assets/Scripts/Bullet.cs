using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D rb = null;

    //Object Pooler
    BulletExplosionPooler bulletExplosionPooler;

    private void Start()
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerBullet");

        bulletExplosionPooler = BulletExplosionPooler._instance;
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
        if(gameObject.name == "PlayerBullet(Clone)")
        {
            bulletExplosionPooler.SpawnFromPool("PlayerBulletExplosion", transform.position, Quaternion.identity);
        }
        else if(gameObject.name == "EnemyBullet(Clone)")
        {
            bulletExplosionPooler.SpawnFromPool("LittleEnemyBulletExplosion", transform.position, Quaternion.identity);
        }
        else if(gameObject.name == "LittleEnemyBullet(Clone)")
        {
            bulletExplosionPooler.SpawnFromPool("AverageEnemyBulletExplosion", transform.position, Quaternion.identity);
        }
        else if(gameObject.name == "MegaEnemyBullet(Clone)")
        {
            bulletExplosionPooler.SpawnFromPool("MegaEnemyBulletExplosion", transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
