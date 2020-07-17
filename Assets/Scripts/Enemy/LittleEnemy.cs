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
            ScoreManager.Instance.CurrentScore += 5;

            _pooler.GetPooledObject("LittleEnemyExplosion", transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }
}
