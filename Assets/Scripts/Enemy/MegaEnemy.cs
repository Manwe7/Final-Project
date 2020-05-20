using UnityEngine;

public class MegaEnemy : Enemy
{
    [SerializeField] private Transform MegaEnemyExplosion = null;

    private void Start()
    {
        _health = Random.Range(50, 60);
    }

    private void LateUpdate()
    {
        if (_health <= 0)
        {
            GameManager.Instance.CurrentScore += 15;
            Instantiate(MegaEnemyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
