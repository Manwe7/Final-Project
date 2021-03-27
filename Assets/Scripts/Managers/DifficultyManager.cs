using Spawners;
using UnityEngine;

namespace Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner[] _bigEnemySpawners;
        [SerializeField] private EnemySpawner[] _flyingEnemySpawners;
    
        private int _difficultyLevel;
    
        private void Awake()
        {
            _difficultyLevel = PlayerPrefs.GetInt(SaveAttributes.DifficultyLevel, 0);

            TurnOffSpawners();
        
            switch (_difficultyLevel)
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
