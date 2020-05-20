using UnityEngine;

public class AverageEnemy : Enemy
{
    [SerializeField] private Transform AverageEnemyExplosion = null;

    private void Start()
    {
        _health = Random.Range(30, 40);
    }

    private void LateUpdate()
    {
        if (_health <= 0)
        {
            GameManager.Instance.CurrentScore += 10;
            Instantiate(AverageEnemyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
