using UnityEngine;

public class MegaEnemy : Enemy
{
    private void OnEnable()
    {
        _health = Random.Range(50, 60);
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
