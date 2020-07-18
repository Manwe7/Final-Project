using UnityEngine;

public class AverageEnemy : Enemy
{
    private void OnEnable()
    {
        _health = Random.Range(30, 40);
    }

    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _scoreManager.AddScore(10);

            _pooler.GetPooledObject(_enemyExplosion.name, transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }
}
