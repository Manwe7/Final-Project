using UnityEngine;

namespace Weapon
{
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

        private Vector3 GetPosition(float offsetX)
        {
            var position = _parent.position;
            return new Vector3(position.x + offsetX, position.y, position.z);
        }

        protected abstract void ChangeWeaponPosition();

        protected void SetToRightSide(float rotationZ)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            transform.position = GetPosition(0.3f);
        }

        protected void SetToLeftSide(float rotationZ)
        {
            transform.rotation = Quaternion.Euler(-180f, 0f, rotationZ);
            transform.position = GetPosition(-0.3f);
        }
    }
}
