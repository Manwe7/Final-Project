using UnityEngine;

public class LittleEnemy : BaseEnemy
{
    protected override int ScoreWeight => 5;

    private void OnEnable()
    {
        _health = Random.Range(10, 20);
        _distance = Random.Range(3f, 12f);
    }
}
