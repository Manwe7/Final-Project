using UnityEngine;

public class LittleEnemy : Enemy
{
    [Header("Barrel")]
    [SerializeField] private Transform barrel = null;

    //Object Pooler
    Pooler pooler;

    private void OnEnable()
    {
        _health = Random.Range(10, 20);

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
            ScoreManager.Instance.CurrentScore += 5;

            //Pooler
            pooler.GetPooledObject("LittleEnemyExplosion", transform.position, Quaternion.identity);            

            gameObject.SetActive(false);
        }
    }

    private void Shooting()
    {
        if (_reloaded)
        {
            //Pooler
            pooler.GetPooledObject("LittleEnemyBullet", barrel.position, barrel.rotation);            

            if (gameObject.activeSelf)
            {
                StartCoroutine(Reload());
            }
        }
    }
}
