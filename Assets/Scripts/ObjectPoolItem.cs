using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public enum ObjectType
    {
        PlayerBullet,
        LittleEnemyBullet,
        AverageEnemyBullet,
        MegaEnemyBullet,
        PlayerBulletExplosion,
        LittleEnemyBulletExplosion,
        AverageEnemyBulletExplosion,
        MegaEnemyBulletExplosion,
        PlayerExplosion, 
        LittleEnemyExplosion,
        AverageEnemyExplosion,
        MegaEnemyExplosion,
        LittleEnemy,
        AverageEnemy,
        MegaEnemy
    }
    public ObjectType objectType;

    public GameObject objectToPool;
    public int amountToPool;
}
