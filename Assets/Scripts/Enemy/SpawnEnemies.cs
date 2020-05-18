using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] GameObject enemy = null;
    [SerializeField] private float movementSpeed=0, movementDistance=0;

    private float _maxTime = 20, _minTime = 5, _time, _spawnTime;
    private float _initialposX;
    private bool _goRight;

    private void Start()
    {
        _initialposX = transform.position.x;
        SetRandomTime();
        _time = _minTime;
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
        Instantiate (enemy, transform.position, transform.rotation);
    }
 
    private void SetRandomTime()
    {
        _spawnTime = Random.Range(_minTime, _maxTime);
    }
}
