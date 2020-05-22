using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //[Header("Object to spawn")]
    //[SerializeField] GameObject enemy = null;

    [Header("Spawn object Name")]
    [SerializeField] string objectName = null;

    [Header("Spawner move settings")]
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float movementDistance = 0;

    [Header("Min And Max reload time")]
    [SerializeField] private float maxTime = 0; 
    [SerializeField] private float minTime = 0;

    private float _time, _spawnTime;
    private float _initialposX;
    private bool _goRight;

    //Object Pooler
    ObjectPooler objectPooler;

    private void Start()
    {
        _initialposX = transform.position.x;
        SetRandomTime();
        _time = minTime;

        objectPooler = ObjectPooler.objectPoolerInstance;
    }

    private void FixedUpdate()
    {
        //Counts up
        _time += Time.deltaTime;    

        //Check if its the right time to spawn the object
        if(_time >= _spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        }

        //Move right and left
        if(transform.position.x > _initialposX + movementDistance)
        {
            _goRight = false;
        }
        else if (transform.position.x < _initialposX - movementDistance)
        {
            _goRight = true;
        }

        if(_goRight == true)
        {
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
        else
        {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        }
    }

    //Spawn enemy
    private void SpawnObject(){
        _time = 0;
        //Instantiate (enemy, transform.position, transform.rotation);
        objectPooler.SpawnFromPool(objectName, transform.position, Quaternion.identity);
    }
 
    private void SetRandomTime()
    {
        _spawnTime = Random.Range(minTime, maxTime);
    }
}
