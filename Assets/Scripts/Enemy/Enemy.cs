using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject EnemyExplosion=null, MegaEnemyExplosion=null, LittleEnemyExplosion=null;
    [SerializeField] private float speed = 0, jumpForce = 0;
    
    private Rigidbody2D _rigidbody2D;
    private GameObject _player;
    private float _health;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //Depending on enemy give _health
        if(gameObject.name == "Enemy(Clone)")
        { _health = Random.Range(30, 60); }
        else if(gameObject.name == "MegaEnemy(Clone)")
        { _health = Random.Range(60, 80); }
        else if(gameObject.name == "LittleEnemy(Clone)")
        { _health = Random.Range(10, 30); }

        try
        {
            //Find _player
            _player = GameObject.FindGameObjectWithTag("_player");
        }
        catch (System.Exception)
        {
            //_player is Dead
        }    
    }

    private void Update()
    {
        //If _health is 0 or less, die and instantiate corresponding particles
        if(_health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            if(gameObject.name == "Enemy(Clone)")
            { 
                GameManager.Instance.CurrentScore += 10;
                Instantiate(EnemyExplosion, transform.position, transform.rotation); 
            }
            else if(gameObject.name == "MegaEnemy(Clone)")
            { 
                GameManager.Instance.CurrentScore += 15;
                Instantiate(MegaEnemyExplosion, transform.position, transform.rotation); 
            }
            else if(gameObject.name == "LittleEnemy(Clone)")
            {
                GameManager.Instance.CurrentScore += 5;
                Instantiate(LittleEnemyExplosion, transform.position, transform.rotation); 
            }
            Destroy(gameObject);
        }

        //Get X-axis
        float move = Input.GetAxis("Horizontal");

        //If _player is alive move towards him
        if(_player != null)
        {
			if (_player.transform.position.x > transform.position.x)
			{ _rigidbody2D.velocity = new Vector2(-move * speed, _rigidbody2D.velocity.y); }
			else 
            { _rigidbody2D.velocity = new Vector2(move * speed, _rigidbody2D.velocity.y); }

            //Jump randomly
			if (Random.Range(0, 1f) < 0.0025)
			{ Jump(); }

            //Shoot randomly
            if (Random.Range(0, 1f) < 0.0025)
            {
                EnemyWeapon enemyWeapon = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<EnemyWeapon>();
                enemyWeapon.Fire();
            }
        }
    }

    private void Jump()
    {
        //Reset velocity on Y-axis
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        //Give force in Y-axis
        _rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void GetDamage(float damage)
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("Hurt");
        _health -= damage;
    }
}
