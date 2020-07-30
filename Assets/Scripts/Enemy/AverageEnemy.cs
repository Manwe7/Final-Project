using UnityEngine;

public class AverageEnemy : BaseEnemy
{
    protected override int _scoreWeight => 10;
    
    private void OnEnable()
    {        
        _health = Random.Range(30, 40);
        _distance = Random.Range(3f, 12f);
    }

}
