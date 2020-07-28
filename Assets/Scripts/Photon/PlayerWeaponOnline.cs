using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PlayerWeaponOnline : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    [SerializeField] private Transform parent = null, barrel = null;

    [SerializeField] private GameObject BulletOnline = null;
    
    [SerializeField] private float reloadTime = 0;

    private Joystick _fixedjoystick = null;
    private RectTransform _joystickHandle = null;    
    private float _offset = 180;

    private bool _reloaded;

    private void Awake()
    {
        _photonView = parent.gameObject.GetComponent<PhotonView>();
        
        if (!_photonView.IsMine) { return; }

        _fixedjoystick = GameObject.Find("Canvas/RotationJoystick").GetComponent<FixedJoystick>();
        _joystickHandle = GameObject.Find("Canvas/RotationJoystick/Handle").GetComponent<RectTransform>();        
    }

    private void Start()
    {
        _reloaded = true;
    }

    private void Update()
    {
        if (!_photonView.IsMine) { return; }

        if (_joystickHandle.anchoredPosition.x < 0 && _joystickHandle.anchoredPosition.x != 0)//(mouse.x < playerScreenPoint.x) 
        {
            LeftSide();
        }
        else if (_joystickHandle.anchoredPosition.x > 0)
        {
            RightSide();
        }

        if (_reloaded)
        {
            Shoot();
            _reloaded = false;
            Invoke("Reload", reloadTime);
        }

    }
    
    [PunRPC]
    public void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate(BulletOnline.name, barrel.position, barrel.rotation);
        bullet.name = parent.gameObject.name + "Bullet";
    }

    private void Reload()
    {
        _reloaded = true;
    }

    private void RightSide()
    {
        Vector3 direction = Vector3.up * _fixedjoystick.Vertical + Vector3.right * _fixedjoystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (rotZ != 0)
        { transform.rotation = Quaternion.Euler(0f, 0f, -rotZ); }

        transform.position = new Vector3(parent.transform.position.x + 0.3f, parent.transform.position.y, parent.transform.position.z);
    }

    private void LeftSide()
    {
        Vector3 direction = Vector3.up * _fixedjoystick.Vertical + Vector3.right * _fixedjoystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (rotZ != 0)
        { transform.rotation = Quaternion.Euler(-180f, 0f, rotZ + _offset); }

        transform.position = new Vector3(parent.transform.position.x - 0.3f, parent.transform.transform.position.y, parent.transform.position.z);
    }
}
