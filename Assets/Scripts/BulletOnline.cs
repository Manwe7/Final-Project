using Mirror;
using UnityEngine;

public class BulletOnline : NetworkBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D rb = null;

    public delegate void Damaged(int damage);        
    public static event Damaged EventDamage;

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

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
        {
            Explode();
        }
        else if (other.gameObject.CompareTag("Player"))
        {            
            CmdPlayerShot();
            Debug.Log(other.gameObject.name + " has been shot");
            Explode();
        }
    }

    [Command]
    void CmdPlayerShot()
    {
        EventDamage(5);      
    }

    [Server]
    void Explode()
    {
        //Depending on bullet instantiate corresponding particles        
        GameObject explosion = pooler.GetPooledObject("PlayerBulletExplosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.SetActive(true);
        
        NetworkManager.Destroy(gameObject);
    }
}
