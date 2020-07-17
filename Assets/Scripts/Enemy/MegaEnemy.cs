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
            ScoreManager.Instance.CurrentScore += 15;

            _pooler.GetPooledObject("MegaEnemyExplosion", transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }    
}
