using UnityEngine;

public class MegaEnemy : Enemy
{
    protected override int _scoreWeight => 15;
    private void OnEnable()
    {
        _health = Random.Range(50, 60);
    }
 
}
