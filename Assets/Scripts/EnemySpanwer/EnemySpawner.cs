using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn object Name")]
    [SerializeField] BaseEnemy _enemyToSpawn;

    [Header("Min And Max reload time")]
    [SerializeField] private float maxTime = 0;
    
    [SerializeField] private float minTime = 0;

    [Header("Player object")]
    [SerializeField] private GameObject _player;

    [Header("Scripts")]
    [SerializeField] private Pooler _pooler;    
    
    [SerializeField] private GameSessionScore _scoreManager;    
    
    [SerializeField] private SoundPlayer _soundPlayer;    
    
    [SerializeField] private CameraShake _cameraShake;

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
        var enemy = _pooler.GetPooledObject(_enemyToSpawn.name, transform.position, Quaternion.identity).GetComponent<BaseEnemy>();
        enemy.Init(_player, _pooler, _cameraShake, _scoreManager, _soundPlayer);
    }
 
    private void SetRandomTime()
    {
        _spawnTime = Random.Range(minTime, maxTime);
    }
}
