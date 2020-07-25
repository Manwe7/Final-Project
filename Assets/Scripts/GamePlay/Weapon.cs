using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _parent;

    protected float _rotationZ;
    protected Vector3 _direction;
    protected float _offset;

    protected void Update() 
    {
        ChangeWeaponPosition();
    }

    protected abstract void ChangeWeaponPosition();
}
