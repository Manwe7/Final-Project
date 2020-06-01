using UnityEngine;
using System.Collections;

public class AverageEnemy : Enemy
{
    [Header("Buttet and barrel")]
    [SerializeField] private Transform barrel = null;

    private int _reloadTime;
    private bool _reloaded;

    //Object Pooler
    Pooler pooler;

    private void Start()
    {
        _health = Random.Range(30, 40);

        StartCoroutine(Reload());

        pooler = Pooler.Instance;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            GameManager.gameManagerInstance.CurrentScore += 10;

            //Pooler
            GameObject explosion = pooler.GetPooledObject("AverageEnemyExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true); //end

            gameObject.SetActive(false);
        }

        if (_reloaded)
        {
            //Pooler
            GameObject explosion = pooler.GetPooledObject("AverageEnemyBullet");
            explosion.transform.position = barrel.transform.position;
            explosion.transform.rotation = barrel.transform.rotation;
            explosion.SetActive(true); //end

            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(3, 5);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
