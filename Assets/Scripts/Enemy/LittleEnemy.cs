using UnityEngine;

public class LittleEnemy : Enemy
{
    private void OnEnable()
    {
        _health = Random.Range(10, 20);
    }
    
    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _scoreManager.AddScore(15);

            _pooler.GetPooledObject(_enemyExplosion.name, transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }
}
