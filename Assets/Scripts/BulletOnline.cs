using Mirror;
using UnityEngine;

public class BulletOnline : NetworkBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody2D rb = null;
   
    private void Start()
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("PlayerBullet");
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
            if (other.name == "LocalPlayer" && gameObject.name == "LocalPlayerBullet")
            { return; }
            if (other.name == "RemotePlayer" && gameObject.name == "RemotePlayerBullet")
            { return; }

            other.gameObject.GetComponent<PlayerOnline>()._health -= 5;
            Debug.Log(other.gameObject.name + " has been shot");
            Explode();
        }
    }

    [Server]
    void Explode()
    {
        /*GameObject explosion = pooler.GetPooledObject("PlayerBulletExplosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.SetActive(true);*/
        
        NetworkManager.Destroy(gameObject);
    }
}
