using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn object Name")]
    [SerializeField] Enemy _enemyToSpawn = null;

    [Header("Min And Max reload time")]
    [SerializeField] private float maxTime = 0;
    [SerializeField] private float minTime = 0;

    [Header("Player object")]
    [SerializeField] private GameObject _player;

    [Header("Pooler object")]
    [SerializeField] private Pooler _pooler;

    [Header("Score Manager")]
    [SerializeField] private ScoreManager _scoreManager;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager _audioManager;

    private float _time, _spawnTime;    

    private void Start()
    {
        SetRandomTime();
        _time = minTime;
    }

    private void Update()
    {        
        _time += Time.deltaTime;

        if(_time >= _spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        }
    }
    
    private void SpawnObject()
    {
        _time = 0;
        var enemy = _pooler.GetPooledObject(_enemyToSpawn.name, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Init(_player, _pooler, _scoreManager, _audioManager);
    }
 
    private void SetRandomTime()
    {
        _spawnTime = Random.Range(minTime, maxTime);
    }
}
