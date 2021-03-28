using Spawners;
using UnityEngine;

namespace Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner[] _bigEnemySpawners;
        [SerializeField] private EnemySpawner[] _flyingEnemySpawners;
    
        public int DifficultyLevel { get; private set; }
    
        private void Awake()
        {
            DifficultyLevel = PlayerPrefs.GetInt(SaveAttributes.DifficultyLevel, 0);

            TurnOffSpawners();
        
            switch (DifficultyLevel)
            {
                case 0:
                    break;
                case 1:
                    EnableBigEnemySpawners(true);
                    break;
                case 2:
                    EnableBigEnemySpawners(true);
                    EnableFlyingEnemySpawners(true);
                    break;
            }
        }

        private void TurnOffSpawners()
        {
            EnableBigEnemySpawners(false);

            EnableFlyingEnemySpawners(false);
        }

        private void EnableBigEnemySpawners(bool status)
        {
            foreach (var spawner in _bigEnemySpawners)
            {
                spawner.enabled = status;
                spawner.gameObject.SetActive(status);
            }
        }

        private void EnableFlyingEnemySpawners(bool status)
        {
            foreach (var spawner in _flyingEnemySpawners)
            {
                spawner.enabled = status;
                spawner.gameObject.SetActive(status);
            }
        }
    }
}
