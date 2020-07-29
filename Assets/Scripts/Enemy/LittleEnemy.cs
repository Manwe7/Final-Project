using UnityEngine;

public class LittleEnemy : Enemy
{
    protected override int _scoreWeight => 5;

    private void OnEnable()
    {
        _health = Random.Range(10, 20);
        _distance = Random.Range(3f, 12f);
    }
}
