using UnityEngine;
using System.Collections;

public class MegaEnemy : Enemy
{
    [Header("Buttet and barrel")]
    [SerializeField] private Transform barrel = null;

    private int _reloadTime;
    private bool _reloaded;

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
        if (_health <= 0)
        {
            GameManager.gameManagerInstance.CurrentScore += 15;

            //Pooler
            GameObject explosion = pooler.GetPooledObject("MegaEnemyExplosion");
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.SetActive(true); //end
            
            gameObject.SetActive(false);
        }

        if (_reloaded)
        {
            //Pooler
            GameObject explosion = pooler.GetPooledObject("MegaEnemyBullet");
            explosion.transform.position = barrel.transform.position;
            explosion.transform.rotation = barrel.transform.rotation;
            explosion.SetActive(true); //end
           
            StartCoroutine(Reload());            
        }
    }

    IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(3, 6);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
