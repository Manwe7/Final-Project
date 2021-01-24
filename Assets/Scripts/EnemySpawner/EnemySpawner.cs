using Enemy;
using ScriptableObjects;
using UnityEngine;

namespace EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn object Name")]
        [SerializeField] private BaseEnemy _enemyToSpawn;    

        [Header("Player object")]
        [SerializeField] private GameObject _player;

        [Header("Scripts")]
        [SerializeField] private Pooler _pooler;
        [SerializeField] private GameSessionScore _scoreManager;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private EnemySpawnerSettings _enemySpawnerSettings;

        private float _time, _spawnTime;    

        private void Start()
        {
            SetSpawnTime();
            _time = 0;
        }

        private void Update()
        {        
            _time += Time.deltaTime;

            if (!(_time >= _spawnTime)) return;
        
            SpawnObject();
            SetSpawnTime();
        }
    
        private void SpawnObject()
        {
            _time = 0;
            BaseEnemy enemy = _pooler.GetPooledObject(_enemyToSpawn.name, transform.position, Quaternion.identity).GetComponent<BaseEnemy>();
            enemy.Init(_player, _pooler, _cameraShake, _scoreManager, _soundPlayer);
        }
 
        private void SetSpawnTime()
        {
            _spawnTime = Random.Range(_enemySpawnerSettings._minTime, _enemySpawnerSettings._maxTime);
        }
    }
}
