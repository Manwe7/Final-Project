using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class Bullet : MonoBehaviour
    {
        [Header("Explosion")]
        [SerializeField] private GameObject BulletExplosion = null;

        [Header("Movement speed")]
        [SerializeField] private float speed = 0;
        
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody2D = null;

        [SerializeField] private PhotonView _photonView;

        private bool exploded;

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.up * speed;
        }

        [PunRPC]
        private void OnTriggerEnter2D(Collider2D other)
        {        
            if(exploded) { return; }

            if (other.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
            {
                Explode();
            }

            if (other.CompareTag("Player"))
            {
                //comparing by string name is not the best option, I would check tags or components of the bject
                if (gameObject.name != other.gameObject.name + "Bullet")
                {
                    other.gameObject.GetComponent<PlayerHealth>().GetDamage(10);
                    Explode();
                }
            }
        }
        
        public void Explode()
        {
            if(!_photonView.IsMine) { return; }

            exploded = true;
            gameObject.SetActive(false);

            PhotonNetwork.Instantiate(BulletExplosion.name, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
