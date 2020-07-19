using UnityEngine;

public class AverageEnemy : Enemy
{
    protected override int _scoreWeight => 10;
    private void OnEnable()
    {
        _health = Random.Range(30, 40);
    }

}
