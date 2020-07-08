using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    private string _explosionName;

    //Object Pooler
    Pooler pooler;

    private void Start()
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerBullet");

        _explosionName = gameObject.name + "Explosion";

        pooler = Pooler.Instance;
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
        if(other.gameObject.CompareTag("Player"))
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
        /*GameObject explosion = pooler.GetPooledObject(_explosionName);
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.SetActive(true);*/

        gameObject.SetActive(false);
    }
}
