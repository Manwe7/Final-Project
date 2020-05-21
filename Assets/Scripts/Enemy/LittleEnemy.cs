using UnityEngine;
using System.Collections;

public class LittleEnemy : Enemy
{
    [Header("Buttet and barrel")]
    [SerializeField] private Transform bullet = null;
    [SerializeField] private Transform barrel = null;

    [Header("Explision particles")]
    [SerializeField] private Transform LittleEnemyExplosion = null;

    private int _reloadTime;
    private bool _reloaded;

    private void Start()
    {
        _health = Random.Range(10, 20);

        StartCoroutine(Reload());
    }

    private void Update()
    {
        if (_health <= 0)
        {
            GameManager.Instance.CurrentScore += 5;
            Instantiate(LittleEnemyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
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
        _reloadTime = Random.Range(3, 7);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }
}
