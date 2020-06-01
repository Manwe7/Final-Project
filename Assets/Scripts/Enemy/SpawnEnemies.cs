using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
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

    private Vector2 _pointA, _pointB;

    //Object Pooler
    Pooler pooler;

    private void Start()
    {
        _initialposX = transform.position.x;
        SetRandomTime();
        _time = minTime;

        pooler = Pooler.Instance;

        _pointA = new Vector2(transform.position.x + movementDistance, transform.position.y);
        _pointB = new Vector2(transform.position.x - movementDistance, transform.position.y);
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
        transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time / movementSpeed, 1));
    }

    //Spawn enemy
    private void SpawnObject(){
        _time = 0;

        //Pooler
        GameObject explosion = pooler.GetPooledObject(objectName);
        explosion.transform.position = transform.position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.SetActive(true); //end
    }
 
    private void SetRandomTime()
    {
        _spawnTime = Random.Range(minTime, maxTime);
    }
}