using ScriptableObjects;
using UnityEngine;

public class EnemySpawnerMovement : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSettings _enemyWeaponSettings;
    
    private Vector2 _pointA, _pointB;

    private void Start()
    {
        var position = transform.position;
        _pointA = new Vector2(position.x + _enemyWeaponSettings._movementDistance, position.y);
        _pointB = new Vector2(position.x - _enemyWeaponSettings._movementDistance, position.y);
    }

    private void Update()
    {        
        transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time / _enemyWeaponSettings._movementSpeed, 1));
    }
}
