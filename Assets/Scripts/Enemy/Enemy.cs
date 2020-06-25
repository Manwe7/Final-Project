using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    private AudioManager _audioManager;

    protected Rigidbody2D _rigidbody2D;
    protected GameObject _player;
    protected float _health;

    protected float _distance;

    protected int _reloadTime;
    protected bool _reloaded;

    private void Awake()
    {
        //Components
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();

        _player = GameObject.FindGameObjectWithTag("Player");                
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

    protected IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(2, 5);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
