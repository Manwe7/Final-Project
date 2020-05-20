using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 0, jumpForce = 0;
    
    protected Rigidbody2D _rigidbody2D;
    protected GameObject _player;
    protected float _health;

    private int _reloadTime;
    private bool _reloaded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        try
        {
            //Find _player
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (System.Exception)
        { } //player is dead

        _reloaded = true;
    }
                 
    private void Update()
    {        
        if (_player != null)
        {
            if (_player.transform.position.x > transform.position.x) //player is in right side
            {
                _rigidbody2D.velocity = new Vector2(speed, 0f);
            }
            else if ((_player.transform.position.x < transform.position.x))
            {
                _rigidbody2D.velocity = new Vector2(-speed, 0f);
            }
        }
        else
        { _rigidbody2D.velocity = new Vector2(0f, 0f); }

        if (_reloaded)
        {
            EnemyWeapon enemyWeapon = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<EnemyWeapon>();
            enemyWeapon.Fire();
            
            _reloaded = false;
            _reloadTime = Random.Range(3, 7);
            Invoke("Reload", _reloadTime);
        }
    }

    private void Reload()
    {
        _reloaded = true;
    }

    protected void GetDamage(float damage)
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("Hurt");
        _health -= damage;
    }
}
