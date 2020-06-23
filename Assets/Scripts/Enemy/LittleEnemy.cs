using UnityEngine;

public class LittleEnemy : Enemy
{
    [Header("Buttet and barrel")]
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
            GameManager.gameManagerInstance.CurrentScore += 5;

            //Pooler
            GameObject explosion = pooler.GetPooledObject("LittleEnemyExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true); //end

            gameObject.SetActive(false);
        }
    }

    private void Shooting()
    {
        if (_reloaded)
        {
            //Pooler
            GameObject explosion = pooler.GetPooledObject("LittleEnemyBullet");
            explosion.transform.position = barrel.transform.position;
            explosion.transform.rotation = barrel.transform.rotation;
            explosion.SetActive(true); //end

            StartCoroutine(Reload());
        }
    }
}
