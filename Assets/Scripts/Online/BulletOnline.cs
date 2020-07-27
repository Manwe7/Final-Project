using UnityEngine;
using Photon.Pun;

public class BulletOnline : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] private GameObject BulletExplosion = null;

    [Header("Movement speed")]
    [SerializeField] private float speed = 0;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private PhotonView _photonView;

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
        if(!_photonView.IsMine) { return; }
        
        gameObject.SetActive(false);

        PhotonNetwork.Instantiate(BulletExplosion.name, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }
}
