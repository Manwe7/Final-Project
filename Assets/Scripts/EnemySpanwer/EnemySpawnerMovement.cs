using UnityEngine;

public class EnemySpawnerMovement : MonoBehaviour
{
    [Header("Spawner move settings")]
    [SerializeField] private float movementSpeed = 0; //make Scripable objects
    [SerializeField] private float movementDistance = 0;

    private Vector2 _pointA, _pointB;

    private void Start()
    {
        _pointA = new Vector2(transform.position.x + movementDistance, transform.position.y);
        _pointB = new Vector2(transform.position.x - movementDistance, transform.position.y);
    }

    private void Update()
    {        
        transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time / movementSpeed, 1));
    }
}
