using UnityEngine;
using Photon.Pun;

public class BulletOnline : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    
    private Rigidbody2D _rigidbody2D = null;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Constant speed
        _rigidbody2D.velocity = transform.up * speed;
    }

    [PunRPC]
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
        {
            Explode();            
        }
        //If player send damage
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == other.gameObject.name + "Bullet")
            {
                return;
            }
            else
            {
                other.gameObject.SendMessageUpwards("GetDamage", 10);
                //CameraShake.ShakeOnce = true;
                Explode();
            }
        }
    }

    public void Explode()
    {
        PhotonNetwork.Destroy(gameObject);
        //Destroy(gameObject);
    }
}
