using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletOnline : NetworkBehaviour
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

    [Client]
    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
        {
            Explode();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            CmdPlayerShot(other.gameObject.name);            
        }
    }

    [Command]
    void CmdPlayerShot(string _id)
    {
        Debug.Log("Player");
        Debug.Log(_id + " has been shot");        
    }

    void Explode()
    {
        //Depending on bullet instantiate corresponding particles        
        GameObject explosion = pooler.GetPooledObject("PlayerBulletExplosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.SetActive(true);

        gameObject.SetActive(false);
    }
}
