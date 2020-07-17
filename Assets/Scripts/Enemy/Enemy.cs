using UnityEngine;

public abstract class Enemy : MonoBehaviour
{ 
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 0;

    private AudioManager _audioManager;
    
    protected GameObject _player;
    protected Pooler _pooler;    
    protected float _health;
    protected float _distance;
    
    private void Awake()
    {        
        _audioManager = FindObjectOfType<AudioManager>();

        _player = GameObject.FindGameObjectWithTag("Player");                
    }

    private void Start()
    {
        _pooler = Pooler.Instance;

        _distance = Random.Range(1f, 12f);
    }

    private void FixedUpdate()
    {
        CheckForPlayerPosition();
    }

    private void CheckForPlayerPosition()
    {
        if (_player != null)
        {
            if (_player.transform.position.x + _distance > transform.position.x) //player is in right side
            {
                _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
            }
            else if ((_player.transform.position.x - _distance < transform.position.x)) //player is in left side
            {
                _rigidbody2D.velocity = new Vector2(-_speed, _rigidbody2D.velocity.y);
            }
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
        }
    }

    public void GetDamage(float damage)
    {
        //Play sound
        _audioManager.Play("Hurt");        
        _health -= damage;
    }    
}
