using UnityEngine;
using Mirror;

public class PlayerOnlineWeapon : NetworkBehaviour
{
    [SerializeField] private Transform parent = null, barrel = null;
    private FixedJoystick joystick = null;
    private RectTransform joystickHandle = null;
    private float reloadTime = 0;
    private float _offset = 180;

    private bool _reloaded;

    //Object Pooler
    Pooler pooler;

    private void Start()
    {
        joystick = GameObject.Find("Canvas/RotationJoystick").GetComponent<FixedJoystick>();
        joystickHandle = GameObject.Find("Canvas/RotationJoystick/Handle").GetComponent<RectTransform>();

        reloadTime = 0.3f;
        _reloaded = true;
        pooler = Pooler.Instance;
    }

    private void Update()
    {
        //Find mouse position for PC
        /*Vector3 mouse = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);*/

        //Change position depending on mouse position
        if (joystickHandle.anchoredPosition.x < 0)//(mouse.x < playerScreenPoint.x) 
        {
            LeftSide();
        }
        else
        {
            RightSide();
        }

        //Shoot
        if (_reloaded)
        {            
            //Pooler
            GameObject bullet = pooler.GetPooledObject("PlayerBullet");
            bullet.transform.position = barrel.transform.position;
            bullet.transform.rotation = barrel.transform.rotation;
            bullet.SetActive(true); //end

            _reloaded = false;
            Invoke("Reload", reloadTime);
        }

    }

    private void Reload()
    {
        _reloaded = true;
    }

    private void RightSide()
    {
        //Rotate and change position to right
        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);*/

        Vector3 direction = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -rotZ);

        transform.position = new Vector3(parent.transform.position.x + 0.3f, parent.transform.position.y, parent.transform.position.z);
    }

    private void LeftSide()
    {
        //Rotate and change position to left
        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, -rotZ + offset);  */

        Vector3 direction = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, rotZ + _offset);

        transform.position = new Vector3(parent.transform.position.x - 0.3f, parent.transform.transform.position.y, parent.transform.position.z);
    }
}
