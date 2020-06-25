using UnityEngine;
using Photon.Pun;

public class BulletOnline : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    
    private Rigidbody2D _rigidbody2D = null;

    PhotonView _photonView;

    private void Awake()
    {
        //you can assign them in Unity Inspector
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        //Constant speed
        _rigidbody2D.velocity = transform.up * speed;
    }

    [PunRPC]
    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
        {
            Explode();
        }
        //If player send damage
        if (other.CompareTag("Player"))
        {
            //comparing by string name is not the best option, I would check tags or components of the bject
            if (gameObject.name != other.gameObject.name + "Bullet")
            {
                other.gameObject.GetComponent<PlayerOnline>().GetDamage(10);
                //CameraShake.ShakeOnce = true;
                Explode();
            }
        }
    }
    
    public void Explode()
    {
        gameObject.SetActive(false);
        if (_photonView.IsMine)
        { 
            PhotonNetwork.Destroy(gameObject); 
        }
        //Destroy(gameObject);
    }
}
