using UnityEngine;

public class MegaEnemy : Enemy
{
    [Header("Barrel")]
    [SerializeField] private Transform barrel = null;

    //Object Pooler
    Pooler pooler;

    private void OnEnable()
    {
        _health = Random.Range(50, 60);

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
            ScoreManager.Instance.CurrentScore += 15;

            //Pooler
            pooler.GetPooledObject("MegaEnemyExplosion", transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }

    private void Shooting()
    {
        if (_reloaded)
        {
            //Pooler
            pooler.GetPooledObject("MegaEnemyBullet", barrel.position, barrel.rotation);            

            if (gameObject.activeSelf)
            {
                StartCoroutine(Reload());
            }
        }
    }
}
