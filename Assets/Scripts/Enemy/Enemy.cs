using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{ 
    [SerializeField] protected GameObject _enemyExplosion;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private EnemyWeaponPosition _weaponPosition;
    [SerializeField] private float _speed = 0;

    private AudioManager _audioManager;
    private CameraShake _cameraShake;

    protected ScoreManager _scoreManager;
    protected GameObject _player;
    protected Pooler _pooler;
    protected float _health;
    protected float _distance;
    protected abstract int _scoreWeight { get; }

    public void Init(GameObject player, Pooler pooler, CameraShake cameraShake, ScoreManager scoreManager, AudioManager audioManager)
    {
        _player = player;
        _pooler = pooler;
        _scoreManager = scoreManager;
        _audioManager = audioManager;
        _cameraShake = cameraShake;
        
        _weapon.Init(player, audioManager, pooler);
        _weaponPosition.Init(player);
    }
    
    private void Start()
    {
        _distance = UnityEngine.Random.Range(3f, 12f);
    }

    private void FixedUpdate()
    {
        CheckForPlayerPosition();
    }

    private void CheckForPlayerPosition()
    {
        if (_player != null)
        {
            var moveSpeed = _speed;
            if(!IsPlayerOnRight())
            {
                moveSpeed *= -1f;
            }
            MoveToPlayer(moveSpeed);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
        }
    }

    private bool IsPlayerOnRight()
    {
        return _player.transform.position.x + _distance > transform.position.x;
    }

    private void MoveToPlayer(float speed)
    {
        _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
    }

    public void ApplyDamage(int damage)
    {
        ShakeCamera();
        _audioManager.Play(SoundNames.Hurt);
        _health -= damage;

        if (_health <= 0)
        {
            _audioManager.Play(SoundNames.EnemyDeath); 
            _scoreManager.AddScore(_scoreWeight);

            _pooler.GetPooledObject(_enemyExplosion.name, transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }

    public void ShakeCamera()
    {
        _cameraShake.ShakeCameraOnce();
    }
}
