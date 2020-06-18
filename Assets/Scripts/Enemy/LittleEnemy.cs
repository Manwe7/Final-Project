using UnityEngine;
using System.Collections;

public class LittleEnemy : Enemy
{
    [Header("Buttet and barrel")]
    [SerializeField] private Transform barrel = null;

    private int _reloadTime;
    private bool _reloaded;

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

    IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(2, 5);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
