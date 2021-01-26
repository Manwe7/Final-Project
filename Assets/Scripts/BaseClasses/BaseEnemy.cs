using System;
using Enemy.Weapon;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaseClasses
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    { 
        [Header("Components")]
        [SerializeField] protected GameObject _enemyExplosion;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] private EnemyWeapon _weapon;
        [SerializeField] private EnemyWeaponPosition _weaponPosition;

        [Header("Stats")]
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpForce;

        private SoundPlayer _soundPlayer;
        private CameraShake _cameraShake;
        private GameSessionScore _scoreManager;
        private GameObject _player;
        private Pooler _pooler;
        private float _jumpTime;
        protected float _health;
        protected float _distance;

        protected abstract int ScoreWeight { get; }

        public void Init(GameObject player, Pooler pooler, CameraShake cameraShake, GameSessionScore scoreManager, SoundPlayer soundPlayer)
        {
            _player = player;
            _pooler = pooler;
            _scoreManager = scoreManager;
            _soundPlayer = soundPlayer;
            _cameraShake = cameraShake;
        
            _weapon.Init(player, pooler);
            _weaponPosition.Init(player);
        }

        private void Update()
        {
            if(_jumpTime > 0)
            {
                _jumpTime -= Time.deltaTime;
            }
            else
            {
                Jump();
                SetJumpTime();
            }
        }

        private void FixedUpdate()
        {
            CheckForPlayerPosition();
        }

        private void SetJumpTime()
        {
            _jumpTime = Random.Range(5, 10);
        }

        private void Jump()
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }

        private void CheckForPlayerPosition()
        {
            if (_player != null)
            {
                float moveSpeed = _speed;
                if(!IsPlayerOnRight() && !IsOnDistance())
                {
                    moveSpeed *= -1f;
                }
                else if(IsOnDistance())
                {
                    _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
                    return;
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
            return Mathf.Round(_player.transform.position.x + _distance) > Mathf.Round(transform.position.x);
        }

        private bool IsOnDistance()
        {
            return Math.Abs(Mathf.Round(_player.transform.position.x + _distance) - Mathf.Round(transform.position.x)) < 0.1f;
        }

        private void MoveToPlayer(float speed)
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }

        public void ApplyDamage(int damage)
        {
            ShakeCamera();
            _soundPlayer.Play(SoundNames.Hurt);
            _health -= damage;

            if (!(_health <= 0)) return;
        
            _soundPlayer.Play(SoundNames.EnemyDeath); 
            _scoreManager.AddScore(ScoreWeight);

            _pooler.GetPooledObject(_enemyExplosion.name, transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }

        private void ShakeCamera()
        {
            _cameraShake.ShakeCameraOnce(1.7f);
        }
    }
}
