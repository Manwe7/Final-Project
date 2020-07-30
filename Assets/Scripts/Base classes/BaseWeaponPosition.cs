using UnityEngine;

public abstract class BaseWeaponPosition : MonoBehaviour
{
    [SerializeField] protected Transform _parent;

    protected float _rotationZ;
    protected Vector3 _direction;
    protected float _offset;

    protected void Update() 
    {
        ChangeWeaponPosition();
    }

    protected Vector3 GetPosition(float offsetX)
    {
        return new Vector3(_parent.position.x + offsetX, _parent.position.y, _parent.position.z);
    }

    protected abstract void ChangeWeaponPosition();
}
