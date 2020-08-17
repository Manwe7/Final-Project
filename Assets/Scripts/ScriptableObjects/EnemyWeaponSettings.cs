using UnityEngine;

[CreateAssetMenu(menuName = "EnemyWeaponSettings")]
public class EnemyWeaponSettings : ScriptableObject
{
    public int _minReloadTime;

    public int _maxReloadTime;
}
