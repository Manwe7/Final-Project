using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "EnemySpawnerSettings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        public float _maxTime;
        public float _minTime;
        public float _movementSpeed;
        public float _movementDistance;
    }
}
