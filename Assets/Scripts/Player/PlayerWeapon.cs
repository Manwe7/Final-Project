using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Joystick _weaponJoystick = null;
    [SerializeField] private RectTransform _joystickHandle = null;
    [SerializeField] private Transform parent = null, barrel = null;
    
    [SerializeField] private float reloadTime = 0;
    
    private float _offset = 180;    
    private bool _reloaded;

    private Pooler pooler;

    private void Start()
    {
        _reloaded = true;
        pooler = Pooler.Instance;
    }

    private void Update()
    {
        ChangeWeaponPosition();

        Shoote();
    }

    private void ChangeWeaponPosition()
    {
        if (_joystickHandle.anchoredPosition.x < 0 && _joystickHandle.anchoredPosition.x != 0)
        {
            SetToLeftSide();
        }
        else if (_joystickHandle.anchoredPosition.x > 0)
        {
            SetToRightSide();
        }
    }

    private void Shoote()
    {
        if (_reloaded)
        {
            pooler.GetPooledObject("PlayerBullet", barrel.position, barrel.rotation);            

            _reloaded = false;
            Invoke("Reload", reloadTime);
        }
    }

    private void Reload()
    {
        _reloaded = true;
    }

    private void SetToRightSide()
    {
        Vector3 direction = Vector3.up * _weaponJoystick.Vertical + Vector3.right * _weaponJoystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (rotZ != 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -rotZ);
        }

        transform.position = new Vector3(parent.transform.position.x + 0.3f, parent.transform.position.y, parent.transform.position.z);
    }

    private void SetToLeftSide()
    {
        Vector3 direction = Vector3.up * _weaponJoystick.Vertical + Vector3.right * _weaponJoystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (rotZ != 0)
        { 
            transform.rotation = Quaternion.Euler(-180f, 0f, rotZ + _offset);
        }

        transform.position = new Vector3(parent.transform.position.x -0.3f, parent.transform.transform.position.y, parent.transform.position.z);
    }
}
