using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 0;// jumpForce = 0;
    
    protected Rigidbody2D _rigidbody2D;
    protected GameObject _player;
    protected float _health;

    protected float _distance;

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
        
    }

    private void Start()
    {
        _distance = Random.Range(1f, 12f);
    }

    private void FixedUpdate()
    {        
        if (_player != null)
        {
            if (_player.transform.position.x + _distance > transform.position.x) //player is in right side
            {
                _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
            }
            else if ((_player.transform.position.x - _distance < transform.position.x)) //player is in right side
            {
                _rigidbody2D.velocity = new Vector2(-speed, _rigidbody2D.velocity.y);
            }
        }        
        else
        { _rigidbody2D.velocity = new Vector2(0f, 0f); }        
    }    

    protected void GetDamage(float damage)
    {
        //Play sound
        FindObjectOfType<AudioManager>().Play("Hurt");
        _health -= damage;
    }
}
