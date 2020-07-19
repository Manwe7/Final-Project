using UnityEngine;

public class PlayerWeaponPosition : MonoBehaviour
{
    [SerializeField] private Joystick _weaponJoystick;
    [SerializeField] private RectTransform _joystickHandle;
    [SerializeField] private Transform _parent;

    private float _offset = 180;
    private Vector2 _direction;
    private float _rotationZ;

    private void Update()
    {
        ChangeWeaponPosition();
    }

    private void ChangeWeaponPosition()
    {
        _direction = Vector3.up * _weaponJoystick.Vertical + Vector3.right * _weaponJoystick.Horizontal;
        _rotationZ = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

        if(_rotationZ == 0) return;

        if (_joystickHandle.anchoredPosition.x < 0)
        {
            SetToLeftSide();
        }
        else if (_joystickHandle.anchoredPosition.x > 0)
        {
            SetToRightSide();
        }
    }

    private void SetToRightSide()
    {       
        transform.rotation = Quaternion.Euler(0f, 0f, -_rotationZ);
        transform.position = new Vector3(_parent.position.x + 0.3f, _parent.position.y, _parent.position.z);
    }

    private void SetToLeftSide()
    {
        transform.rotation = Quaternion.Euler(-180f, 0f, _rotationZ + _offset);
        transform.position = new Vector3(_parent.position.x - 0.3f, _parent.transform.position.y, _parent.position.z);
    }
}
