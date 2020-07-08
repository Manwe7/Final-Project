using UnityEngine;

public class AverageEnemy : Enemy
{
    [Header("Barrel")]
    [SerializeField] private Transform barrel = null;

    //Object Pooler
    Pooler pooler;

    private void OnEnable()
    {
        _health = Random.Range(30, 40);

        StartCoroutine(Reload());

        pooler = Pooler.Instance;
    }

    private void Update()
    {
        CheckHealth();
        Shooting();   
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            ScoreManager.Instance.CurrentScore += 10;

            //Pooler
            pooler.GetPooledObject("AverageEnemyExplosion", transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }

    private void Shooting()
    {
        if (_reloaded)
        {
            //Pooler
            pooler.GetPooledObject("AverageEnemyBullet", barrel.position, barrel.rotation);            

            if (gameObject.activeSelf)
            {
                StartCoroutine(Reload());
            }
        }
    }
}
