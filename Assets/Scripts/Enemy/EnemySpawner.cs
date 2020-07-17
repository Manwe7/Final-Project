using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn object Name")]
    [SerializeField] string objectName = null;    

    [Header("Min And Max reload time")]
    [SerializeField] private float maxTime = 0; 
    [SerializeField] private float minTime = 0;

    private float _time, _spawnTime;    

    Pooler pooler;

    private void Start()
    {
        SetRandomTime();
        _time = minTime;

        pooler = Pooler.Instance;        
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
        pooler.GetPooledObject(objectName, transform.position, Quaternion.identity);        
    }
 
    private void SetRandomTime()
    {
        _spawnTime = Random.Range(minTime, maxTime);
    }
}
