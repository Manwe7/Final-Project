﻿using UnityEngine;

public class LittleEnemy : Enemy
{
    [SerializeField] private Transform LittleEnemyExplosion = null;

    private void Start()
    {
        _health = Random.Range(10, 30);
    }


        /*GameManager.Instance.CurrentScore += 5;
        Instantiate(LittleEnemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);*/

}
