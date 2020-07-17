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
            ScoreManager.Instance.CurrentScore += 10;

            _pooler.GetPooledObject("AverageEnemyExplosion", transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }
}
