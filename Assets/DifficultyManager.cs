using UnityEngine;
using Spawners;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _bigEnemySpawners;
    [SerializeField] private EnemySpawner[] _flyingEnemySpawners;
    
    private int _difficultyLevels;
    
    private void Awake()
    {
        _difficultyLevels = PlayerPrefs.GetInt(SaveAttributes.DifficultyLevel, 0);

        TurnOffSpawners();
        
        switch (_difficultyLevels)
        {
            case 0:
                break;
            case 1:
                EnableBigEnemySpawners(true);
                break;
            case 2:
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
        }
    }

    private void EnableFlyingEnemySpawners(bool status)
    {
        foreach (var spawner in _flyingEnemySpawners)
        {
            spawner.enabled = status;
        }
    }
}
