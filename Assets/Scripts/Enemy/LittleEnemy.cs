using UnityEngine;

public class LittleEnemy : Enemy
{
    protected override int _scoreWeight => 5;
    private void OnEnable()
    {
        _health = Random.Range(10, 20);
    }
}
