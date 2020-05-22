using UnityEngine;
using System.Collections;

public class AverageEnemy : Enemy
{
    [Header("Buttet and barrel")]
    [SerializeField] private Transform bullet = null;
    [SerializeField] private Transform barrel = null;

    private int _reloadTime;
    private bool _reloaded;

    //Object Pooler
    ObjectPooler objectPooler;

    private void Start()
    {
        _health = Random.Range(30, 40);

        StartCoroutine(Reload());

        objectPooler = ObjectPooler.objectPoolerInstance;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            GameManager.gameManagerInstance.CurrentScore += 10;
            objectPooler.SpawnFromPool("AverageEnemyExplosion", transform.position, Quaternion.identity);            
            gameObject.SetActive(false);
        }

        if (_reloaded)
        {
            Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
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
