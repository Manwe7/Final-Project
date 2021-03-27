using UnityEngine;

namespace Spawner
{
    public class EnemySpawnerMovement : MonoBehaviour
    {
        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;

        private Vector2 _pointA, _pointB;

        private void Start()
        {
            var position = transform.position;
            _pointA = new Vector2(position.x - _minDistance, position.y);
            _pointB = new Vector2(position.x + _maxDistance, position.y);
        }

        private void Update()
        {        
            transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time / 10, 1));
        }
    }
}
