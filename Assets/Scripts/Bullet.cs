using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] GameObject PlayerBulletExp=null, LittleEnemyBulletExp=null, EnemyBulletExp=null, MegaEnemyBulletExp=null;
    
    // Start is called before the first frame update
    void Start()
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerBullet");
    }

    // Update is called once per frame
    void FixedUpdate()
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
            Instantiate(PlayerBulletExp, transform.position, transform.rotation);
        }
        else if(gameObject.name == "EnemyBullet(Clone)")
        {
            Instantiate(EnemyBulletExp, transform.position, transform.rotation);
        }
        else if(gameObject.name == "LittleEnemyBullet(Clone)")
        {
            Instantiate(LittleEnemyBulletExp, transform.position, transform.rotation);
        }
        else if(gameObject.name == "MegaEnemyBullet(Clone)")
        {
            Instantiate(MegaEnemyBulletExp, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
