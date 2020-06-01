using UnityEngine;
using System.Collections;

public class AverageEnemy : Enemy
{
    [Header("Buttet and barrel")]
    [SerializeField] private Transform barrel = null;

    private int _reloadTime;
    private bool _reloaded;

    //Object Pooler
    ExplosionPooler explosionPooler;
    BulletPooler bulletPooler;

    private void Start()
    {
        _health = Random.Range(30, 40);

        StartCoroutine(Reload());

        explosionPooler = ExplosionPooler._instance;
        bulletPooler = BulletPooler._instance;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            GameManager.gameManagerInstance.CurrentScore += 10;
            explosionPooler.SpawnFromPool("AverageEnemyExplosion", transform.position, Quaternion.identity);            
            gameObject.SetActive(false);
        }

        if (_reloaded)
        {
            bulletPooler.SpawnFromPool("AverageEnemyBullet", barrel.transform.position, barrel.transform.rotation);
            //Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
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
